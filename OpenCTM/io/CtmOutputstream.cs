using System;
using System.IO;

namespace OpenCTM
{
	public class CtmOutputstream : BinaryWriter
	{
		
    	private readonly int compressionLevel;
		
		public CtmOutputstream (Stream output) : this(5, output)
		{			
		}
		
		public CtmOutputstream (int compressionLevel, Stream output) : base(output)
		{
			this.compressionLevel = compressionLevel;
		}
		
		public void writeString(String text){
	        if (text != null) {
	            writeLittleInt(text.Length);
	            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
				Write(enc.GetBytes(text));
	        } else {
	            writeLittleInt(0);
	        }
	    }
	
	    public void writeLittleInt(int v){
			
	        Write((byte)(v & 0xFF));
			//>>> was used in the original code
	        Write((byte)((v >> 8) & 0xFF));
	        Write((byte)((v >> 16) & 0xFF));
	        Write((byte)((v >> 24) & 0xFF));
	    }
	
	    public void writeLittleIntArray(int[] v){
	        foreach (int a in v) {
	            writeLittleInt(a);
	        }
	    }
	
	    public void writeLittleFloat(float v){
	        CtmInputStream.IntFloat a = new CtmInputStream.IntFloat();
			a.FloatValue = v;
			writeLittleInt(a.IntValue);
	    }
	
	    public void writeLittleFloatArray(float[] v){
	        foreach (float a in v) {
	            writeLittleFloat(a);
	        }
	    }
	
//	    public void writePackedInts(int[] data, int count, int size, boolean signed) throws IOException {
//	        assert data.length >= count * size : "The data to be written is smaller"
//	                + " as stated by other parameters. Needed: " + (count * size) + " Provided: " + data.length;
//	        // Allocate memory for interleaved array
//	        byte[] tmp = new byte[count * size * 4];
//	
//	        // Convert integers to an interleaved array
//	        for (int i = 0; i < count; ++i) {
//	            for (int k = 0; k < size; ++k) {
//	                int value = data[i * size + k];
//	                // Convert two's complement to signed magnitude?
//	                if (signed) {
//	                    value = value < 0 ? -1 - (value << 1) : value << 1;
//	                }
//	                interleavedInsert(value, tmp, i + k * count, count * size);
//	            }
//	        }
//	
//	        writeCompressedData(tmp);
//	    }
//	
//	    public void writePackedFloats(float[] data, int count, int size) throws IOException {
//	        assert data.length >= count * size : "The data to be written is smaller"
//	                + " as stated by other parameters. Needed: " + (count * size) + " Provided: " + data.length;
//	        // Allocate memory for interleaved array
//	        byte[] tmp = new byte[count * size * 4];
//	
//	        // Convert floats to an interleaved array
//	        for (int x = 0; x < count; ++x) {
//	            for (int y = 0; y < size; ++y) {
//	                int value = Float.floatToIntBits(data[x * size + y]);
//	                interleavedInsert(value, tmp, x + y * count, count * size);
//	            }
//	        }
//	        writeCompressedData(tmp);
//	    }
//	
//	    public static void interleavedInsert(int value, byte[] data, int offset, int stride) {
//	        data[offset + 3 * stride] = (byte) (value & 0xff);
//	        data[offset + 2 * stride] = (byte) ((value >> 8) & 0xff);
//	        data[offset + stride] = (byte) ((value >> 16) & 0xff);
//	        data[offset] = (byte) ((value >> 24) & 0xff);
//	    }
//	
//	    public void writeCompressedData(byte[] data) throws IOException {
//	        //some magic size as in the OpenCTM reference implementation
//	        ByteArrayOutputStream bout = new ByteArrayOutputStream(1000 + data.length);
//	
//	        Encoder enc = new Encoder();
//	        enc.setEndMarkerMode(true);
//	        if (compressionLevel <= 5) {
//	            enc.setDictionarySize(1 << (compressionLevel * 2 + 14));
//	        } else if (compressionLevel == 6) {
//	            enc.setDictionarySize(1 << 25);
//	        } else {
//	            enc.setDictionarySize(1 << 26);
//	        }
//	        enc.setNumFastBytes(compressionLevel < 7 ? 32 : 64);
//	        
//	        enc.code(new ByteArrayInputStream(data), bout, data.length, -1, null);
//	//        try (OutputStream lzout = new LzmaOutputStream(bout, new CustomWrapper(enc))) {
//	//            lzout.write(data);
//	//        }
//	
//	        //This is the custom way of OpenCTM to write the LZMA properties
//	        this.writeLittleInt(bout.size());
//	        enc.writeCoderProperties(this);
//	        bout.writeTo(this);
//	//        write(data);
//	    }
	}
}

