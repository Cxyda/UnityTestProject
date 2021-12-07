// Implementation has been found here: https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a

using UnityEngine.Networking;

namespace Utility
{
	public static class UnityWebRequestAsyncOperationExtensionMethods
	{
		public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
		{
			return new UnityWebRequestAwaiter(asyncOp);
		}	
	}
}