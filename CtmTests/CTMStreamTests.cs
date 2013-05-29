using System;
using System.IO;
using NUnit.Framework;
using OpenCTM;

namespace CtmTests
{
	[TestFixture()]
	public class CTMStreamTests
	{
		[Test()]
		public void WriteCompressedData ()
		{
			byte[] data = new byte[]{1,2,3,4,5,6,7,8,9,10};
			//write
			MemoryStream memory = new MemoryStream();
			CtmOutputStream outS = new CtmOutputStream(memory);
			outS.writeCompressedData(data);
			
			//read
			memory.Seek(0, SeekOrigin.Begin);
			Stream readMemory = new MemoryStream(memory.ToArray());
			CtmInputStream inS = new CtmInputStream(readMemory);
			
			Assert.AreEqual(data, inS.readCompressedData(data.Length));
			Assert.AreEqual(memory.Length, readMemory.Position);
		}
	}
}

