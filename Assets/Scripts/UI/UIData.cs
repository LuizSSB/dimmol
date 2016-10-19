/// @file UIData.Instance.Instancecs
/// @brief Details to be specified
/// @author FvNano/LBT team
/// @author Marc Baaden <baaden@smplinux.de>
/// @date   2013-4
///
/// Copyright Centre National de la Recherche Scientifique (CNRS)
///
/// contributors :
/// FvNano/LBT team, 2010-13
/// Marc Baaden, 2010-13
///
/// baaden@smplinux.de
/// http://www.baaden.ibpc.fr
///
/// This software is a computer program based on the Unity3D game engine.
/// It is part of UnityMol, a general framework whose purpose is to provide
/// a prototype for developing molecular graphics and scientific
/// visualisation applications. More details about UnityMol are provided at
/// the following URL: "http://unitymol.sourceforge.net". Parts of this
/// source code are heavily inspired from the advice provided on the Unity3D
/// forums and the Internet.
///
/// This software is governed by the CeCILL-C license under French law and
/// abiding by the rules of distribution of free software. You can use,
/// modify and/or redistribute the software under the terms of the CeCILL-C
/// license as circulated by CEA, CNRS and INRIA at the following URL:
/// "http://www.cecill.info".
/// 
/// As a counterpart to the access to the source code and rights to copy, 
/// modify and redistribute granted by the license, users are provided only 
/// with a limited warranty and the software's author, the holder of the 
/// economic rights, and the successive licensors have only limited 
/// liability.
///
/// In this respect, the user's attention is drawn to the risks associated 
/// with loading, using, modifying and/or developing or reproducing the 
/// software by the user in light of its specific status of free software, 
/// that may mean that it is complicated to manipulate, and that also 
/// therefore means that it is reserved for developers and experienced 
/// professionals having in-depth computer knowledge. Users are therefore 
/// encouraged to load and test the software's suitability as regards their 
/// requirements in conditions enabling the security of their systems and/or 
/// data to be ensured and, more generally, to use and operate it in the 
/// same conditions as regards security.
///
/// The fact that you are presently reading this means that you have had 
/// knowledge of the CeCILL-C license and that you accept its terms.
///
/// $Id: UIData.Instance.Instancecs 647 2014-08-06 12:20:04Z tubiana $
///
/// References : 
/// If you use this code, please cite the following reference : 	
/// Z. Lv, A. Tek, F. Da Silva, C. Empereur-mot, M. Chavent and M. Baaden:
/// "Game on, Science - how video game technology may help biologists tackle
/// visualization challenges" (2013), PLoS ONE 8(3):e57990.
/// doi:10.1371/journal.pone.0057990
///
/// If you use the HyperBalls visualization metaphor, please also cite the
/// following reference : M. Chavent, A. Vanel, A. Tek, B. Levy, S. Robert,
/// B. Raffin and M. Baaden: "GPU-accelerated atom and dynamic bond visualization
/// using HyperBalls, a unified algorithm for balls, sticks and hyperboloids",
/// J. Comput. Chem., 2011, 32, 2924
///
using System.Linq;

namespace UI
{

	using UnityEngine;
	using System.Collections;
	
	/** !WiP Includes FLAGS of GUI.
	 * Like types of atoms and bounding representation.
	 * Var with //TODO !EXPLANATION! need extra explanation.<BR>
	 * Unity3D Doc :<BR>
	 * <A HREF="http://docs.unity3d.com/Documentation/ScriptReference/Color.html">Color</A>
	 */
	[System.Serializable]
	public class UIData {
//		public bool standalone=true;
		
		//Initial molecule to load from resources
		public string init_molecule = "";

		#if UNITY_EDITOR
//			public string server_url = "http://172.27.0.170/";
			public string server_url = "http://www.shaman.ibpc.fr/umolweb/";
		#else
			public string server_url = "";
		#endif
//			public string server_url = "http://localhost:8888/";
		
		
		public bool fetchPDBFile = false;
		public bool isConfirm = false;
		public bool changeStructure = false;
		public bool hasMoleculeDisplay = false;
		public bool hasResidues = false;
		public bool hasChains = false;
		public bool isclear=false;
		public bool isOpenFile=false;
		public bool isParticlesInitialized = false;

		// Luiz:
		public bool stateChanged = false;
		public bool autoChangingState = false;
		
		public bool isCubeLoaded = false;
		public bool isSphereLoaded = false;
		public bool isHBallLoaded = false;
		
		public bool isRenderDictInit = false;
		public bool isTexturesMenuListInit = false;
		
		public bool readHetAtom = true;
		public bool readWater = false;
		public bool connectivity_calc = true;
		public bool connectivity_PDB = false;
		public bool resetDisplay=false;
		public bool isCubeToSphere=false;
		public bool isSphereToCube=true;
		
		public bool resetBondDisplay=false;
		
		public bool toggleMouse = true;
		public bool toggleKey = false;
			
		public bool toggleClip =true;
		public bool togglePlane=false;
		
		public bool toggleGray =false;
		public bool toggleColor=true;
		
		public bool backGroundIs = false;
		public bool backGroundNo = true;
		
		public bool cameraStop = false;
		
		public bool cameraStop2 = false;

		public bool loginSucess=false;
		
		public AtomType atomtype=AtomType.particleball;

		public BondType bondtype=BondType.nobond;
		
		public bool EnableUpdate=true;
		
		public bool interactive=false;
		
		public bool resetInteractive=false;
		
		public bool meshcombine=false;
		
		public bool resetMeshcombine=false;
		
		public bool fileBrowser;
		
		public bool switchmode=false;
		
		public bool hballsmoothmode=false;
		
		public bool grayscalemode = false;
		
		public bool hiddenUI=false;
		
		public bool hiddenUIbutFPS=false;
		
		public bool hiddenCamera=false;

		public bool up_part=true;
		public bool down_part=false;
		
		public bool openAllMenu=false;

		public bool changingState=false;
		public bool openBound=false;
		
		public bool secondarystruct=false;
		public bool toggle_bf = false;
		public bool isRescale = false;
		public bool toggle_SS = false;
		public bool ssColChain = false;
		public bool ssColStruct = false;
		public bool ssDivCol = false;
		public bool surfColChain = false;
		public bool surfColHydroKD = false;
		public bool surfColHydroEng = false;
		public bool surfColHydroWO = false;
		public bool surfColHydroEis = false;
		public bool surfColHydroHW = false;
		public bool surfColPChim = false;
		public bool surfColBF = false;
		public bool isGLIC = false;
		public bool spread_tree = false;

		public bool firststruct=true;
		
		public bool toggleSurf=true;
		public bool toggleBfac=false;

		// Guided navigation mode
		public bool guided=false;
		// Optimal view mode
		public bool optim_view=false;
		public Vector3 optim_view_start_point;
		public float start_time;
		
		public enum AtomType {
			cube=0,
			sphere=1,	
			hyperball=2,
			raycasting=3,
			billboard=4,
			rcbillboard=5,
			hbbillboard=6,
			rcsprite=7,
			multihyperball=8,
			combinemeshball=9,
			particleball=10,
			particleballalphablend=11,
			noatom = 12
		}
		
		public enum BondType {
			cube=0,
			line=1,
			hyperstick=2,
			tubestick=3,
			bbhyperstick=4,
			particlestick=5,
			nobond=6	
		}

		public enum FFType {
			atomic = 0,
			HiRERNA = 1
		}

		public bool loadHireRNA = false;
		public FFType ffType = FFType.atomic;

		// Luiz:
		private static UIData sInstance = new UIData();
		public static UIData Instance {
			get {
				return sInstance;
			}
		}
		public string ChosenPdbContents; //{ get; set; }
		private static string[] mUpdateParts;
		public string[] SerializeInParts() {
			string serialized = JsonUtility.ToJson(this);
			int length = (int) System.Math.Ceiling (serialized.Length / 4000.0);
			string[] parts = new string[length];
			for (int i = 0, idxPart = 0; i < serialized.Length; i += 4000, ++idxPart) {
				parts [idxPart] = idxPart
					+ "/"
					+ length
					+ "$"
					+ serialized.Substring (i, System.Math.Min (4000, serialized.Length - i));
			}
			return parts;
		}
		public static bool DeserializePart(string serializedDataChunk) {
			int indexOfSlash = serializedDataChunk.IndexOf ("/");
			int indexOfDollar = serializedDataChunk.IndexOf ("$");

			int totalParts = int.Parse (
				serializedDataChunk.Substring(indexOfSlash + 1, indexOfDollar - (indexOfSlash + 1))
			);
			int idxPart = int.Parse (
				serializedDataChunk.Substring(0, indexOfSlash)
			);

			if (mUpdateParts == null
//				|| mUpdateParts.Length != totalParts
//				|| (mUpdateParts[idxPart] != null
//					&& mUpdateParts[idxPart] != serializedDataChunk)
			) {
				mUpdateParts = new string[totalParts];
			}

			mUpdateParts [idxPart] = serializedDataChunk.Substring (indexOfDollar + 1);

			if (mUpdateParts.Any (p => p == null)) {
				return false;
			}

			string serialized = string.Join (string.Empty, mUpdateParts);
			sInstance = JsonUtility.FromJson<UIData> (serialized);
			mUpdateParts = null;
			return true;
		}
	}
}
