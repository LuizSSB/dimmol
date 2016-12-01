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
	[ClearableSingleton]
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
		public float stateTime;
		private bool _autoChangingState = false;
				public bool autoChangingState {
			get { return _autoChangingState; }
			set {
				if (value != _autoChangingState) {
					stateTime = 0f;
				}
				_autoChangingState = this.ProcessPropertyChanged("autoChangingState", _autoChangingState, value);
			}
		}
		public bool MustDie { get; set; }
		
		public bool isCubeLoaded = false;
		public bool isSphereLoaded = false;
		public bool isHBallLoaded = false;
		
		public bool isRenderDictInit = false;
		public bool isTexturesMenuListInit = false;
		
		public bool readHetAtom = true;
		public bool readWater = false;
		public bool connectivity_calc = true;
		public bool connectivity_PDB = false;

		// Luiz:
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

		public bool openBound=false;

		private bool _secondarystruct = false;

		public bool secondarystruct {
			get { return _secondarystruct; }
			set { _secondarystruct = this.ProcessPropertyChanged("secondarystruct", _secondarystruct, value); }
		}
		private bool _toggle_bf = false;
		public bool toggle_bf {
			get { return _toggle_bf; }
			set { _toggle_bf = this.ProcessPropertyChanged("toggle_bf", _toggle_bf, value); }
		}
		private bool _isRescale = false;
		public bool isRescale {
			get { return _isRescale; }
			set { _isRescale = this.ProcessPropertyChanged("isRescale", _isRescale, value); }
		}
		private bool _toggle_SS = false;
		public bool toggle_SS {
			get { return _toggle_SS; }
			set { _toggle_SS = this.ProcessPropertyChanged("toggle_SS", _toggle_SS, value); }
		}
		private bool _ssColChain = false;
		public bool ssColChain {
			get { return _ssColChain; }
			set { _ssColChain = this.ProcessPropertyChanged("ssColChain", _ssColChain, value); }
		}
		private bool _ssColStruct = false;
		public bool ssColStruct {
			get { return _ssColStruct; }
			set { _ssColStruct = this.ProcessPropertyChanged("ssColStruct", _ssColStruct, value); }
		}
		private bool _ssDivCol = false;
		public bool ssDivCol {
			get { return _ssDivCol; }
			set { _ssDivCol = this.ProcessPropertyChanged("ssDivCol", _ssDivCol, value); }
		}
		private bool _surfColChain = false;
		public bool surfColChain {
			get { return _surfColChain; }
			set { _surfColChain = this.ProcessPropertyChanged("surfColChain", _surfColChain, value); }
		}
		private bool _surfColHydroKD = false;
		public bool surfColHydroKD {
			get { return _surfColHydroKD; }
			set { _surfColHydroKD = this.ProcessPropertyChanged("surfColHydroKD", _surfColHydroKD, value); }
		}
		private bool _surfColHydroEng = false;
		public bool surfColHydroEng {
			get { return _surfColHydroEng; }
			set { _surfColHydroEng = this.ProcessPropertyChanged("surfColHydroEng", _surfColHydroEng, value); }
		}
		private bool _surfColHydroWO = false;
		public bool surfColHydroWO {
			get { return _surfColHydroWO; }
			set { _surfColHydroWO = this.ProcessPropertyChanged("surfColHydroWO", _surfColHydroWO, value); }
		}
		private bool _surfColHydroEis = false;
		public bool surfColHydroEis {
			get { return _surfColHydroEis; }
			set { _surfColHydroEis = this.ProcessPropertyChanged("surfColHydroEis", _surfColHydroEis, value); }
		}
		private bool _surfColHydroHW = false;
		public bool surfColHydroHW {
			get { return _surfColHydroHW; }
			set { _surfColHydroHW = this.ProcessPropertyChanged("surfColHydroHW", _surfColHydroHW, value); }
		}
		private bool _surfColPChim = false;
		public bool surfColPChim {
			get { return _surfColPChim; }
			set { _surfColPChim = this.ProcessPropertyChanged("surfColPChim", _surfColPChim, value); }
		}
		private bool _surfColBF = false;
		public bool surfColBF {
			get { return _surfColBF; }
			set { _surfColBF = this.ProcessPropertyChanged("surfColBF", _surfColBF, value); }
		}
		private bool _isGLIC = false;
		public bool isGLIC {
			get { return _isGLIC; }
			set { _isGLIC = this.ProcessPropertyChanged("isGLIC", _isGLIC, value); }
		}
		private bool _spread_tree = false;
		public bool spread_tree {
			get { return _spread_tree; }
			set { _spread_tree = this.ProcessPropertyChanged("spread_tree", _spread_tree, value); }
		}
		private bool _toggleBfac = false;
		public bool toggleBfac {
			get { return _toggleBfac; }
			set { _toggleBfac = this.ProcessPropertyChanged("toggleBfac", _toggleBfac, value); }
		}
		//public bool secondarystruct=false;
		//public bool toggle_bf = false;
		//public bool isRescale = false;
		//public bool toggle_SS = false;
		//public bool ssColChain = false;
		//public bool ssColStruct = false;
		//public bool ssDivCol = false;
		//public bool surfColChain = false;
		//public bool surfColHydroKD = false;
		//public bool surfColHydroEng = false;
		//public bool surfColHydroWO = false;
		//public bool surfColHydroEis = false;
		//public bool surfColHydroHW = false;
		//public bool surfColPChim = false;
		//public bool surfColBF = false;
		//public bool isGLIC = false;
		//public bool spread_tree = false;

		public bool firststruct=true;
		
		public bool toggleSurf=true;
		//public bool toggleBfac=false;

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
		private static UIData sInstance;
		public static UIData Instance {
			get {
				return sInstance = sInstance ?? new UIData();
			}
		}
		public string ChosenPdbContents; //{ get; set; }

		public static void SetNewData(UIData newData) {
			sInstance = newData;
		}
	}
}
