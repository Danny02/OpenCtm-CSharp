using System;

namespace OpenCTM
{
	public interface MeshEncoder
	{
		void encode(Mesh m, CtmOutputstream output);

	    int getTag();
	
	    int getFormatVersion();
	}
}

