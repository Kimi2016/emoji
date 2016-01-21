/*
               #########                       
              ############                     
              #############                    
             ##  ###########                   
            ###  ###### #####                  
            ### #######   ####                 
           ###  ########## ####                
          ####  ########### ####               
         ####   ###########  #####             
        #####   ### ########   #####           
       #####   ###   ########   ######         
      ######   ###  ###########   ######       
     ######   #### ##############  ######      
    #######  #####################  ######     
    #######  ######################  ######    
   #######  ###### #################  ######   
   #######  ###### ###### #########   ######   
   #######    ##  ######   ######     ######   
   #######        ######    #####     #####    
    ######        #####     #####     ####     
     #####        ####      #####     ###      
      #####       ###        ###      #        
        ###       ###        ###              
         ##       ###        ###               
__________#_______####_______####______________

                我们的未来没有BUG              
* ==============================================================================
* Filename: GZipCompress
* Created:  2016/1/21 15:05:28
* Author:   HaYaShi ToShiTaKa
* Purpose:  
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using SevenZip.Compression.LZMA;
public class SevenZipCompress {
	public static byte[] Compress(byte[] bytes) {
		Encoder coder = new Encoder();

		using (MemoryStream input = new MemoryStream(bytes)) {
			using (MemoryStream output = new MemoryStream()) {
				// Write the encoder properties
				coder.WriteCoderProperties(output);

				// Write the decompressed file size.
				output.Write(BitConverter.GetBytes(input.Length), 0, 8);

				// Encode the file.
				coder.Code(input, output, input.Length, -1, null);

				return output.ToArray();
			}
		}

	}
	public static byte[] Decompress(Byte[] bytes) {
		Decoder coder = new Decoder();

		using (MemoryStream input = new MemoryStream(bytes)) {
			using (MemoryStream output = new MemoryStream()) {
				// Read the decoder properties
				byte[] properties = new byte[5];
				input.Read(properties, 0, 5);

				// Read in the decompress file size.
				byte[] fileLengthBytes = new byte[8];
				input.Read(fileLengthBytes, 0, 8);
				long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

				// Decompress the file.
				coder.SetDecoderProperties(properties);
				coder.Code(input, output, input.Length, fileLength, null);
				
				return output.ToArray();
			}
		}


	}
}