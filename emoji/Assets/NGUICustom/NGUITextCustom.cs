//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

static public class NGUITextCustom {
	static public Vector3 GetExactCharacterRowList(string text, List<int> rowList, int picIndex) {
		Vector3 result = new Vector3(0, 0, 0);
		if (string.IsNullOrEmpty(text)) text = " ";

		NGUIText.Prepare(text);

		float fullSize = NGUIText.fontSize * NGUIText.fontScale;
		float x = 0f, y = 0f, maxX = 0f;
		float lastY = y + 1.76f;

		int lastIndex = -1;
		int textLength = text.Length, ch = 0, prev = 0;

		for (int i = 0; i < textLength; ++i) {
			ch = text[i];
			if (ch == '\n') {
				if (x > maxX) maxX = x;

				x = 0;
				y += NGUIText.finalLineHeight;
				prev = 0;
				continue;
			}
			else if (ch < ' ') {
				prev = 0;
				continue;
			}

			if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i)) {
				--i;
				continue;
			}

			// See if there is a symbol matching this text
			BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, textLength) : null;

			if (symbol == null) {
				float gw = NGUIText.GetGlyphWidth(ch, prev);

				if (gw != 0f) {
					float w = gw + NGUIText.finalSpacingX;

					if (Mathf.RoundToInt(x + w) > NGUIText.regionWidth) {
						if (x == 0f) return result;

						x = 0f;
						y += NGUIText.finalLineHeight;
						prev = 0;
						--i;
						continue;
					}
					if (lastY != y) {
						rowList.Add(lastIndex + 1);
					}
					if (i == picIndex) {
						result = new Vector3(x, -y);
					}
					prev = ch;
					x += w;
				}
			}
			else {
				float w = symbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;

				if (Mathf.RoundToInt(x + w) > NGUIText.regionWidth) {
					if (x == 0f) return result;

					x = 0f;
					y += NGUIText.finalLineHeight;
					prev = 0;
					--i;
					continue;
				}

				if (lastY != y) {
					rowList.Add(lastIndex + 1);
				}
				if (i == picIndex) {
					result = new Vector3(x, -y);
				}
				i += symbol.sequence.Length - 1;
				x += w;
				prev = 0;
			}
			lastY = y;
			lastIndex = i;
		}
		
		return result;
	}
	
	static public bool WrapText(string text, out string finalText, bool wrapLineColors = false) {
		return WrapText(text, out finalText, false, wrapLineColors, true);
	}

	static public bool WrapText(string text, out string finalText, bool keepCharCount, bool wrapLineColors, bool eastern) {
		if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 1 || NGUIText.finalLineHeight < 1f) {
			finalText = "";
			return false;
		}

		float height = (NGUIText.maxLines > 0) ? Mathf.Min(NGUIText.regionHeight, NGUIText.finalLineHeight * NGUIText.maxLines) : NGUIText.regionHeight;
		int maxLineCount = (NGUIText.maxLines > 0) ? NGUIText.maxLines : 1000000;
		maxLineCount = Mathf.FloorToInt(Mathf.Min(maxLineCount, height / NGUIText.finalLineHeight) + 0.01f);

		if (maxLineCount == 0) {
			finalText = "";
			return false;
		}

		if (string.IsNullOrEmpty(text)) text = " ";
		NGUIText.Prepare(text);

		StringBuilder sb = new StringBuilder();
		int textLength = text.Length;
		float remainingWidth = NGUIText.regionWidth;
		int start = 0, offset = 0, lineCount = 1, prev = 0;
		bool lineIsEmpty = true;
		bool fits = true;

		Color c = NGUIText.tint;
		int subscriptMode = 0;  // 0 = normal, 1 = subscript, 2 = superscript
		bool bold = false;
		bool italic = false;
		bool underline = false;
		bool strikethrough = false;
		bool ignoreColor = false;

		if (!NGUIText.useSymbols) wrapLineColors = false;
		if (wrapLineColors) NGUIText.mColors.Add(c);

		// Run through all characters
		for (; offset < textLength; ++offset) {
			char ch = text[offset];

			// New line character -- start a new line
			if (ch == '\n') {
				if (lineCount == maxLineCount) break;
				remainingWidth = NGUIText.regionWidth;

				// Add the previous word to the final string
				if (start < offset) sb.Append(text.Substring(start, offset - start + 1));
				else sb.Append(ch);

				if (wrapLineColors) {
					for (int i = 0; i < NGUIText.mColors.size; ++i)
						sb.Insert(sb.Length - 1, "[-]");

					for (int i = 0; i < NGUIText.mColors.size; ++i) {
						sb.Append("[");
						sb.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
						sb.Append("]");
					}
				}

				lineIsEmpty = true;
				++lineCount;
				start = offset + 1;
				prev = 0;
				continue;
			}
			// When encoded symbols such as [RrGgBb] or [-] are encountered, skip past them
			if (NGUIText.encoding) {
				if (!wrapLineColors) {
					if (NGUIText.ParseSymbol(text, ref offset)) {
						--offset;
						continue;
					}
				}
				else if (NGUIText.ParseSymbol(text, ref offset, NGUIText.mColors, NGUIText.premultiply, ref subscriptMode, ref bold,
					ref italic, ref underline, ref strikethrough, ref ignoreColor)) {
					if (ignoreColor) {
						c = NGUIText.mColors[NGUIText.mColors.size - 1];
						c.a *= NGUIText.mAlpha * NGUIText.tint.a;
					}
					else {
						c = NGUIText.tint * NGUIText.mColors[NGUIText.mColors.size - 1];
						c.a *= NGUIText.mAlpha;
					}

					for (int b = 0, bmax = NGUIText.mColors.size - 2; b < bmax; ++b)
						c.a *= NGUIText.mColors[b].a;

					--offset;
					continue;
				}
			}

			// See if there is a symbol matching this text
			BMSymbol symbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, offset, textLength) : null;

			// Calculate how wide this symbol or character is going to be
			float glyphWidth;

			if (symbol == null) {
				// Find the glyph for this character
				float w = NGUIText.GetGlyphWidth(ch, prev);
				if (w == 0f) continue;
				glyphWidth = NGUIText.finalSpacingX + w;
			}
			else glyphWidth = NGUIText.finalSpacingX + symbol.advance * NGUIText.fontScale;

			// Reduce the width
			remainingWidth -= glyphWidth;

			// If this marks the end of a word, add it to the final string.
			if (NGUIText.IsSpace(ch) && !eastern && start < offset) {
				int end = offset - start + 1;

				// Last word on the last line should not include an invisible character
				if (lineCount == maxLineCount && remainingWidth <= 0f && offset < textLength) {
					char cho = text[offset];
					if (cho < ' ' || NGUIText.IsSpace(cho)) --end;
				}

				sb.Append(text.Substring(start, end));
				lineIsEmpty = false;
				start = offset + 1;
				prev = ch;
			}

			// Doesn't fit?
			if (Mathf.RoundToInt(remainingWidth) < 0) {
				// Can't start a new line
				if (lineIsEmpty || lineCount == maxLineCount) {
					// This is the first word on the line -- add it up to the character that fits
					sb.Append(text.Substring(start, Mathf.Max(0, offset - start)));
					bool space = NGUIText.IsSpace(ch);
					if (!space && !eastern) fits = false;

					if (wrapLineColors && NGUIText.mColors.size > 0) sb.Append("[-]");

					if (lineCount++ == maxLineCount) {
						start = offset;
						break;
					}

					if (keepCharCount) NGUIText.ReplaceSpaceWithNewline(ref sb);
					else NGUIText.EndLine(ref sb);

					if (wrapLineColors) {
						for (int i = 0; i < NGUIText.mColors.size; ++i)
							sb.Insert(sb.Length - 1, "[-]");

						for (int i = 0; i < NGUIText.mColors.size; ++i) {
							sb.Append("[");
							sb.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
							sb.Append("]");
						}
					}

					// Start a brand-new line
					lineIsEmpty = true;

					start = offset;
					remainingWidth = NGUIText.regionWidth - glyphWidth;

					prev = 0;
				}
				else {
					// Revert the position to the beginning of the word and reset the line
					lineIsEmpty = true;
					remainingWidth = NGUIText.regionWidth;
					offset = start - 1;
					prev = 0;

					if (lineCount++ == maxLineCount) break;
					if (keepCharCount) NGUIText.ReplaceSpaceWithNewline(ref sb);
					else NGUIText.EndLine(ref sb);

					if (wrapLineColors) {
						for (int i = 0; i < NGUIText.mColors.size; ++i)
							sb.Insert(sb.Length - 1, "[-]");

						for (int i = 0; i < NGUIText.mColors.size; ++i) {
							sb.Append("[");
							sb.Append(NGUIText.EncodeColor(NGUIText.mColors[i]));
							sb.Append("]");
						}
					}
					continue;
				}
			}
			else prev = ch;

			// Advance the offset past the symbol
			if (symbol != null) {
				offset += symbol.length - 1;
				prev = 0;
			}
		}

		if (start < offset) sb.Append(text.Substring(start, offset - start));
		if (wrapLineColors && NGUIText.mColors.size > 0) sb.Append("[-]");
		finalText = sb.ToString();
		NGUIText.mColors.Clear();
		return fits && ((offset == textLength) || (lineCount <= Mathf.Min(NGUIText.maxLines, maxLineCount)));
	}
}