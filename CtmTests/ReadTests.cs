using System;
using System.IO;
using NUnit.Framework;
using OpenCTM;

namespace CtmTests
{
	[TestFixture]
	public class ReadTests
	{
		private static float[] vert = new float[]{	0,0,0,
													1,0,0,
													1,1,0,
													0,1,0};
		private static int[] ind = new int[]{0,1,2,0,2,3};
		private static Mesh quad= new Mesh(vert, null, ind, new AttributeData[0], new AttributeData[0]);
		
		[Test]
		public void rawTest()
		{
			testEncoder(new RawEncoder());
		}
		
		[Test]
		public void mg1Test()
		{
			testEncoder(new MG1Encoder());
		}
		
		[Test]
		public void mg2Test()
		{
			testEncoder(new MG2Encoder());
		}
		
		[Test]
		public void readTest()
		{
			FileStream file = new FileStream("resources/brunnen.ctm", FileMode.Open);
			CtmFileReader reader = new CtmFileReader(file);
			
			Mesh m = reader.decode();
			
			m.checkIntegrity();
		}
		
		private void testEncoder(MeshEncoder encoder)
		{
			MemoryStream memory = new MemoryStream();
			CtmFileWriter writer = new CtmFileWriter(memory, encoder);
			writer.encode(quad, null);
			
			memory.Seek(0, SeekOrigin.Begin);
			Stream readMemory = new MemoryStream(memory.ToArray());
			CtmFileReader reader = new CtmFileReader(readMemory);
			Mesh m = reader.decode();
			
			m.checkIntegrity();
			
			if(encoder as MG2Encoder == null)
				Assert.IsTrue(quad.Equals(m));
			else{
				Assert.IsTrue(quad.getTriangleCount() == m.getTriangleCount());
				Assert.IsTrue(quad.getVertexCount() == m.getVertexCount());
			}
		}
	}
}

