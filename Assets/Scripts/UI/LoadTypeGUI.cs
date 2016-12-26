/// @file LoadTypeGUI.Instance.cs
/// @brief This class contains a collection of functions that define GUI windows.
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
/// $Id: LoadTypeGUI.Instance.cs 213 2013-04-06 21:13:42Z baaden $
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
using Config;
using UnityEngine;
using System.Collections;
using Molecule.Model;
using Molecule.View;

namespace UI {
	
	// Can't be static because GUIMoleculeController isn't, thought it probably ought to be.
	// 
	// Luiz: well, check this out: the only reason this class inherited from GUIMoleculeController was because, due to
	// the variable/if-driven aspect of this source-code, it would save up some words when accessing static variables 
	// from GUIMoleculeController. Both classes were almost pretty much static and shared almost nothing in common.
	// To put it simply: noobs in action.
	// Luiz: had to cut-off this inheritance shit, because I needed both classes to be singletons. The reason for this 
	// is that we're dealing with the possibility of the user traversing through scenes and the infinite static
	// variables here hindered the possibility of going back to the Molecule3D scene, since the static variables would 
	// fuck up something somewhere. With singletons, I can "reset" these variables, effectively reseting the scene.
	[ClearableSingleton]
	public class LoadTypeGUI {
		//Luiz: singleton pattern
		private static LoadTypeGUI sInstance;
		public static LoadTypeGUI Instance {
			get {
				return sInstance = sInstance ?? new LoadTypeGUI();
			}
		}
		
		// Size values for the orthographic camera
		public float minOrthoSize = 1f ;
		public float maxOrthoSize = 60f ;
		public float orthoSize = 10f; // size of the orthographic camera

		// Luiz:
		//Parameters for SugarRibbons
//		public float RibbonsThickness = 0.15f;
//		public float OxySphereSize = 1f;
//		public float OxySphereSizeCheck = 1f;
//		public float thickness_Little=1.8f;
//		public float thickness_BIG=1f;
//		public float thickness_bond_6_C1_C4=0.2f; 
//		public float thickness_6_other=0.16f; 
//		public float thickness_bond_5=0.2f;
//		public float lighter_color_factor_bond=0.35f;
//		public float lighter_color_factor_bond_check=0.35f;
//		public float lighter_color_factor_ring=0.35f;
//		public float lighter_color_factor_ring_check=0.35f;
//		public int ColorationModeRing=0;
//		public int ColorationModeBond=0;
		private float _RibbonsThickness = .15f;
		public float RibbonsThickness {
			get { return _RibbonsThickness; }
			set { _RibbonsThickness = this.ProcessPropertyChanged("RibbonsThickness", _RibbonsThickness, value); }
		}
		private float _OxySphereSize = 1f;
		public float OxySphereSize {
			get { return _OxySphereSize; }
			set { _OxySphereSize = this.ProcessPropertyChanged("OxySphereSize", _OxySphereSize, value); }
		}
		private float _OxySphereSizeCheck = 1f;
		public float OxySphereSizeCheck {
			get { return _OxySphereSizeCheck; }
			set { _OxySphereSizeCheck = this.ProcessPropertyChanged("OxySphereSizeCheck", _OxySphereSizeCheck, value); }
		}
		private float _thickness_Little = 1.8f;
		public float thickness_Little {
			get { return _thickness_Little; }
			set { _thickness_Little = this.ProcessPropertyChanged("thickness_Little", _thickness_Little, value); }
		}
		private float _thickness_BIG = 1f;
		public float thickness_BIG {
			get { return _thickness_BIG; }
			set { _thickness_BIG = this.ProcessPropertyChanged("thickness_BIG", _thickness_BIG, value); }
		}
		private float  _thickness_bond_6_C1_C4 = .2f;
		public float  thickness_bond_6_C1_C4 {
			get { return _thickness_bond_6_C1_C4; }
			set { _thickness_bond_6_C1_C4 = this.ProcessPropertyChanged("thickness_bond_6_C1_C4", _thickness_bond_6_C1_C4, value); }
		}
		private float _thickness_6_other = .16f;
		public float thickness_6_other {
			get { return _thickness_6_other; }
			set { _thickness_6_other = this.ProcessPropertyChanged("thickness_6_other", _thickness_6_other, value); }
		}
		private float _thickness_bond_5 = .2f;
		public float thickness_bond_5 {
			get { return _thickness_bond_5; }
			set { _thickness_bond_5 = this.ProcessPropertyChanged("thickness_bond_5", _thickness_bond_5, value); }
		}
		private float _lighter_color_factor_bond = .2f;
		public float lighter_color_factor_bond {
			get { return _lighter_color_factor_bond; }
			set { _lighter_color_factor_bond = this.ProcessPropertyChanged("lighter_color_factor_bond", _lighter_color_factor_bond, value); }
		}
		private float _lighter_color_factor_bond_check = .2f;
		public float lighter_color_factor_bond_check {
			get { return _lighter_color_factor_bond_check; }
			set { _lighter_color_factor_bond_check = this.ProcessPropertyChanged("lighter_color_factor_bond_check", _lighter_color_factor_bond_check, value); }
		}
		private float _lighter_color_factor_ring = .2f;
		public float lighter_color_factor_ring {
			get { return _lighter_color_factor_ring; }
			set { _lighter_color_factor_ring = this.ProcessPropertyChanged("lighter_color_factor_ring", _lighter_color_factor_ring, value); }
		}
		private float _lighter_color_factor_ring_check = .2f;
		public float lighter_color_factor_ring_check {
			get { return _lighter_color_factor_ring_check; }
			set { _lighter_color_factor_ring_check = this.ProcessPropertyChanged("lighter_color_factor_ring_check", _lighter_color_factor_ring_check, value); }
		}
		private int _ColorationModeRing = 0;
		public int ColorationModeRing {
			get { return _ColorationModeRing; }
			set { _ColorationModeRing = this.ProcessPropertyChanged("ColorationModeRing", _ColorationModeRing, value); }
		}
		private int _ColorationModeBond = 0;
		public int ColorationModeBond {
			get { return _ColorationModeBond; }
			set { _ColorationModeBond = this.ProcessPropertyChanged("ColorationModeBond", _ColorationModeBond, value); }
		}

		//definition of sugarRibons and RingBlending, to avoid create them to each frame
		public SugarRibbons SR;
		public RingBlending ringblending;
		
		// Colors for buttons in secondary structure menu
		// Note : LoadTypeGUI and GUIDisplay are separated randomly. Don't know why some UI features are in one or another. Here, it forces us to set this public.
		// TODO : Fusion LoadTypeGUI and GUIDisplay ? (and GUIMoleculeController ???) or at least do some cleaning in those
		public Color[] helixButtonNew = new Color[200];
		public Texture2D helixButton = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] sheetButtonNew = new Color[200];
		public Texture2D sheetButton = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] coilButtonNew = new Color[200];
		public Texture2D coilButton = new Texture2D(20,10,TextureFormat.ARGB32,false);

		public Texture2D chainbuttonA = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] chainbuttonAnew = new Color[200];
		public Texture2D chainbuttonB = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] chainbuttonBnew = new Color[200];
		public Texture2D chainbuttonC = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] chainbuttonCnew = new Color[200];
		public Texture2D chainbuttonD = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] chainbuttonDnew = new Color[200];
		public Texture2D chainbuttonE = new Texture2D(20,10,TextureFormat.ARGB32,false);
		public Color[] chainbuttonEnew = new Color[200];
		
		// TODO : Make C-Alpha trace and his color/texture work again
/*		// Textures for buttons in AdvOptions 
		private Texture2D chainATex;
		private Texture2D chainBTex;
		private Texture2D chainCTex;
		private Texture2D chainDTex;
		private Texture2D chainETex;
		private Texture2D chainFTex;
		private Texture2D chainGTex;
		private Texture2D chainHTex;
		
		// Colors for the chains
		private ColorObject chainAColor = new ColorObject(Color.white);
		private ColorObject chainBColor = new ColorObject(Color.white);
		private ColorObject chainCColor = new ColorObject(Color.white);
		private ColorObject chainDColor = new ColorObject(Color.white);
		private ColorObject chainEColor = new ColorObject(Color.white);
		private ColorObject chainFColor = new ColorObject(Color.white);
		private ColorObject chainGColor = new ColorObject(Color.white);
		private ColorObject chainHColor = new ColorObject(Color.white);
		
		
		// Color arrays for the button Textures
		private Color[] chainAColors = new Color[200];
		private Color[] chainBColors = new Color[200];
		private Color[] chainCColors = new Color[200];
		private Color[] chainDColors = new Color[200];
		private Color[] chainEColors = new Color[200];
		private Color[] chainFColors = new Color[200];
		private Color[] chainGColors = new Color[200];
		private Color[] chainHColors = new Color[200];
		
		// Set to true when the chainXColors arrays have been initalized
		private bool chainColorsInit = false ;
		*/
		private string _SymmetryOriginX = "34.3444";
		public string SymmetryOriginX {
			get { return _SymmetryOriginX; }
			set { _SymmetryOriginX = this.ProcessPropertyChanged("SymmetryOriginX", _SymmetryOriginX, value); }
		}
		private string _SymmetryOriginY = "4.29016";
		public string SymmetryOriginY {
			get { return _SymmetryOriginY; }
			set { _SymmetryOriginY = this.ProcessPropertyChanged("SymmetryOriginY", _SymmetryOriginY, value); }
		}
		private string _SymmetryOriginZ = "69.0832";
		public string SymmetryOriginZ {
			get { return _SymmetryOriginZ; }
			set { _SymmetryOriginZ = this.ProcessPropertyChanged("SymmetryOriginZ", _SymmetryOriginZ, value); }
		}
		private string _SymmetryDirectionX = "0.446105";
		public string SymmetryDirectionX {
			get { return _SymmetryDirectionX; }
			set { _SymmetryDirectionX = this.ProcessPropertyChanged("SymmetryDirectionX", _SymmetryDirectionX, value); }
		}
		private string _SymmetryDirectionY = "0.00135695";
		public string SymmetryDirectionY {
			get { return _SymmetryDirectionY; }
			set { _SymmetryDirectionY = this.ProcessPropertyChanged("SymmetryDirectionY", _SymmetryDirectionY, value); }
		}
		private string _SymmetryDirectionZ = "-0.894949";
		public string SymmetryDirectionZ {
			get { return _SymmetryDirectionZ; }
			set { _SymmetryDirectionZ = this.ProcessPropertyChanged("SymmetryDirectionZ", _SymmetryDirectionZ, value); }
		}
		private string _TargetX = "16.32";
		public string TargetX {
			get { return _TargetX; }
			set { _TargetX = this.ProcessPropertyChanged("TargetX", _TargetX, value); }
		}
		private string _TargetY = "-1.42";
		public string TargetY {
			get { return _TargetY; }
			set { _TargetY = this.ProcessPropertyChanged("TargetY", _TargetY, value); }
		}
		private string _TargetZ = "-18.17";
		public string TargetZ {
			get { return _TargetZ; }
			set { _TargetZ = this.ProcessPropertyChanged("TargetZ", _TargetZ, value); }
		}
		private string _CameraDistance = "-18.17";
		public string CameraDistance {
			get { return _CameraDistance; }
			set { _CameraDistance = this.ProcessPropertyChanged("CameraDistance", _CameraDistance, value); }
		}
//		public string SymmetryOriginX = "34.3444";
//		public string SymmetryOriginY = "4.29016";
//		public string SymmetryOriginZ = "69.0832";
//		public string SymmetryDirectionX = "0.446105";
//		public string SymmetryDirectionY = "0.00135695";
//		public string SymmetryDirectionZ = "-0.894949";
//		public string CameraDistance = "20.0";
		
		public bool showOriginAxe = true;
		public bool originThere = true;

		// Luiz:
		private bool _toggle_RING_BLENDING = false;
		public bool toggle_RING_BLENDING {
			get { return _toggle_RING_BLENDING; }
			set {
				if (toggle_RING_BLENDING == value)
					return;
				
				bool ssToggled = toggle_RING_BLENDING;
				_toggle_RING_BLENDING = ChangeManager.ProcessPropertyChanged (typeof(LoadTypeGUI), "toggle_RING_BLENDING", _toggle_RING_BLENDING, value);

				if (!ssToggled && toggle_RING_BLENDING) { // enabling the SugarBlending
					ringblending = new RingBlending ();
					ringblending.CreateRingBlending ();
				} else {
					if (ssToggled && !toggle_RING_BLENDING) { // destroying the SugarBlending
						GameObject[] blendObjs = GameObject.FindGameObjectsWithTag ("RingBlending");
						foreach (GameObject blendobj in blendObjs)
							GameObject.Destroy (blendobj);
					}
				}
			}
		}
		private bool _toggle_TWISTER = false;
		public bool toggle_TWISTER {
			get { return _toggle_TWISTER; }
			set {
				if (value == _toggle_TWISTER)
					return;

				bool twToggled = toggle_TWISTER;

				_toggle_TWISTER = ChangeManager.ProcessPropertyChanged (typeof(LoadTypeGUI), "toggle_TWISTER", _toggle_TWISTER, value);

				if (!twToggled && toggle_TWISTER) { // enabling the ribbons
					SR = new SugarRibbons (GUIMoleculeController.Instance.toggle_SUGAR_ONLY);
					//Twister twisters = new Twister();
					//twisters.CreateTwister();
					SR.createSugarRibs (RibbonsThickness, GUIMoleculeController.Instance.toggle_SUGAR_ONLY, thickness_Little, thickness_BIG, 
						thickness_bond_6_C1_C4, thickness_6_other, thickness_bond_5, lighter_color_factor_ring, lighter_color_factor_bond,
						ColorationModeRing, ColorationModeBond, BondColor, RingColor, OxySphereSize, OxySphereColor);
					GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;
					toggle_SHOW_HB_NOT_SUGAR = false;
					toggle_SHOW_HB_W_SR = false;
					toggle_HIDE_HYDROGEN = false;

					//Initialize bond & ring color to an "empty" color.
					BondColorcheck.color = Color.white;
					BondColor.color = Color.white;
					RingColorcheck.color = Color.white;
					RingColor.color = Color.white;
					OxySphereColorCheck.color = Color.red;

				} else if (twToggled && !toggle_TWISTER) { // destroying the ribbons
					GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;
					GameObject[] SugarRibbons;
					SugarRibbons = GameObject.FindGameObjectsWithTag ("SugarRibbons_RING_BIG");
					foreach (GameObject SugarRibbon in SugarRibbons)
						Object.Destroy (SugarRibbon);
					SugarRibbons = GameObject.FindGameObjectsWithTag ("SugarRibbons_RING_little");
					foreach (GameObject SugarRibbon in SugarRibbons)
						Object.Destroy (SugarRibbon);
					SugarRibbons = GameObject.FindGameObjectsWithTag ("SugarRibbons_BOND");
					foreach (GameObject SugarRibbon in SugarRibbons)
						Object.Destroy (SugarRibbon);
				}
			}
		}
		private bool _toggle_HIDE_HYDROGEN = false;
		public bool toggle_HIDE_HYDROGEN {
			get { return _toggle_HIDE_HYDROGEN; }
			set {
				if (value == _toggle_HIDE_HYDROGEN)
					return;

				bool hydroToggled = toggle_HIDE_HYDROGEN;
				_toggle_HIDE_HYDROGEN = ChangeManager.ProcessPropertyChanged (typeof(LoadTypeGUI), "toggle_HIDE_HYDROGEN", _toggle_HIDE_HYDROGEN, value);
				if(!hydroToggled && toggle_HIDE_HYDROGEN)
					showHydrogens(false);
				else if (hydroToggled && !toggle_HIDE_HYDROGEN)
					showHydrogens(true);
			}
		}
		private bool _toggle_SHOW_HB_W_SR = false;
		public bool toggle_SHOW_HB_W_SR {
			get { return _toggle_SHOW_HB_W_SR; }
			set {
				if (value == toggle_SHOW_HB_W_SR)
					return;

				_toggle_SHOW_HB_W_SR = ChangeManager.ProcessPropertyChanged (typeof(LoadTypeGUI), "toggle_SHOW_HB_W_SR", _toggle_SHOW_HB_W_SR, value);

				bool hb_w_sb_toggled = toggle_SHOW_HB_W_SR;
				if(!hb_w_sb_toggled && toggle_SHOW_HB_W_SR)
					show_HyperBalls_Sugar(false);
				else if (hb_w_sb_toggled && !toggle_SHOW_HB_W_SR)
					show_HyperBalls_Sugar(true);
			}
		}
		private bool _toggle_SHOW_HB_NOT_SUGAR = false;
		public bool toggle_SHOW_HB_NOT_SUGAR {
			get { return _toggle_SHOW_HB_NOT_SUGAR; }
			set {
				if (toggle_SHOW_HB_NOT_SUGAR == value)
					return;

				bool hb_not_sugar_toggled = toggle_SHOW_HB_NOT_SUGAR;
				_toggle_SHOW_HB_NOT_SUGAR = ChangeManager.ProcessPropertyChanged (typeof(LoadTypeGUI), "toggle_SHOW_HB_NOT_SUGAR", _toggle_SHOW_HB_NOT_SUGAR, value);

				if(!hb_not_sugar_toggled && toggle_SHOW_HB_NOT_SUGAR)
					Hide_No_Sugar_Hiperballs(false);
				else if (hb_not_sugar_toggled && !toggle_SHOW_HB_NOT_SUGAR)
					Hide_No_Sugar_Hiperballs(true);
			}
		}
		private Quaternion NA_SCCROT = new Quaternion (-0.1f, 0.1f, 0.0f, -1.0f);
		private Vector3 NA_SCCPOS = new Vector3 (0.4f, 1.8f, -12.0f);

		private bool _toggle_VE_BLUR = false;
		public bool toggle_VE_BLUR {
			get { return _toggle_VE_BLUR; }
			set { _toggle_VE_BLUR = this.ProcessPropertyChanged("toggle_VE_BLUR", _toggle_VE_BLUR, value); }
		}
		private bool _toggle_VE_SSAO = false;
		public bool toggle_VE_SSAO {
			get { return _toggle_VE_SSAO; }
			set { _toggle_VE_SSAO = this.ProcessPropertyChanged("toggle_VE_SSAO", _toggle_VE_SSAO, value); }
		}
		private bool _toggle_VE_DOF = false;
		public bool toggle_VE_DOF {
			get { return _toggle_VE_DOF; }
			set { _toggle_VE_DOF = this.ProcessPropertyChanged("toggle_VE_DOF", _toggle_VE_DOF, value); }
		}
		private bool _toggle_VE_CREASE = false;
		public bool toggle_VE_CREASE {
			get { return _toggle_VE_CREASE; }
			set { _toggle_VE_CREASE = this.ProcessPropertyChanged("toggle_VE_CREASE", _toggle_VE_CREASE, value); }
		}
		private bool _toggle_VE2_VORTX = false;
		public bool toggle_VE2_VORTX {
			get { return _toggle_VE2_VORTX; }
			set { _toggle_VE2_VORTX = this.ProcessPropertyChanged("toggle_VE2_VORTX", _toggle_VE2_VORTX, value); }
		}
		private bool _toggle_VE2_TWIRL = false;
		public bool toggle_VE2_TWIRL {
			get { return _toggle_VE2_TWIRL; }
			set { _toggle_VE2_TWIRL = this.ProcessPropertyChanged("toggle_VE2_TWIRL", _toggle_VE2_TWIRL, value); }
		}
		private bool _toggle_VE2_SEPIA = false;
		public bool toggle_VE2_SEPIA {
			get { return _toggle_VE2_SEPIA; }
			set { _toggle_VE2_SEPIA = this.ProcessPropertyChanged("toggle_VE2_SEPIA", _toggle_VE2_SEPIA, value); }
		}
		private bool _toggle_VE2_NOISE = false;
		public bool toggle_VE2_NOISE {
			get { return _toggle_VE2_NOISE; }
			set { _toggle_VE2_NOISE = this.ProcessPropertyChanged("toggle_VE2_NOISE", _toggle_VE2_NOISE, value); }
		}
		private bool _toggle_VE2_GRAYS = false;
		public bool toggle_VE2_GRAYS {
			get { return _toggle_VE2_GRAYS; }
			set { _toggle_VE2_GRAYS = this.ProcessPropertyChanged("toggle_VE2_GRAYS", _toggle_VE2_GRAYS, value); }
		}
		private bool _toggle_VE2_GLOW = false;
		public bool toggle_VE2_GLOW {
			get { return _toggle_VE2_GLOW; }
			set { _toggle_VE2_GLOW = this.ProcessPropertyChanged("toggle_VE2_GLOW", _toggle_VE2_GLOW, value); }
		}
		private bool _toggle_VE2_EDGE = false;
		public bool toggle_VE2_EDGE {
			get { return _toggle_VE2_EDGE; }
			set { _toggle_VE2_EDGE = this.ProcessPropertyChanged("toggle_VE2_EDGE", _toggle_VE2_EDGE, value); }
		}
		private bool _toggle_VE2_CONTR = false;
		public bool toggle_VE2_CONTR {
			get { return _toggle_VE2_CONTR; }
			set { _toggle_VE2_CONTR = this.ProcessPropertyChanged("toggle_VE2_CONTR", _toggle_VE2_CONTR, value); }
		}
		private bool _toggle_VE2_CCORR = false;
		public bool toggle_VE2_CCORR {
			get { return _toggle_VE2_CCORR; }
			set { _toggle_VE2_CCORR = this.ProcessPropertyChanged("toggle_VE2_CCORR", _toggle_VE2_CCORR, value); }
		}
		private bool _toggle_VE2_BLUR2 = false;
		public bool toggle_VE2_BLUR2 {
			get { return _toggle_VE2_BLUR2; }
			set { _toggle_VE2_BLUR2 = this.ProcessPropertyChanged("toggle_VE2_BLUR2", _toggle_VE2_BLUR2, value); }
		}
		private bool _toggle_VE2_DREAM = false;
		public bool toggle_VE2_DREAM {
			get { return _toggle_VE2_DREAM; }
			set { _toggle_VE2_DREAM = this.ProcessPropertyChanged("toggle_VE2_DREAM", _toggle_VE2_DREAM, value); }
		}
//		private bool toggle_VE_BLUR = false;
//		private bool toggle_VE_SSAO = false;
//		private bool toggle_VE_DOF = false;
//		private bool toggle_VE_CREASE = false;
//		private bool toggle_VE2_VORTX = false;
//		private bool toggle_VE2_TWIRL = false;
//		private bool toggle_VE2_SEPIA = false;
//		private bool toggle_VE2_NOISE = false;
//		private bool toggle_VE2_GRAYS = false;
//		private bool toggle_VE2_GLOW = false;
//		private bool toggle_VE2_EDGE = false;
//		private bool toggle_VE2_CONTR = false;
//		private bool toggle_VE2_CCORR = false;
//		private bool toggle_VE2_BLUR2 = false;
//		private bool toggle_VE2_DREAM = false;
		private string[] ve2_grays_ramps = {"grayscale ramp", "grayscale ramp inverse"}; // Ramps for grayscale effect
		private int ve2_grays_rampn = 1;
		private int _ve2_grays_rampc = 1;
		public int ve2_grays_rampc {
			get { return _ve2_grays_rampc; }
			set { _ve2_grays_rampc = this.ProcessPropertyChanged("ve2_grays_rampc", _ve2_grays_rampc, value); }
		}
//		private int ve2_grays_rampc = 1;
		private string[] ve2_ccorr_ramps = {"oceangradient", "nightgradient"}; // Ramps for color correction effect
		private int ve2_ccorr_rampn = 1;
		private int _ve2_ccorr_rampc = 0;
		public int ve2_ccorr_rampc {
			get { return _ve2_ccorr_rampc; }
			set { _ve2_ccorr_rampc = this.ProcessPropertyChanged("ve2_ccorr_rampc", _ve2_ccorr_rampc, value); }
		}
//		private int ve2_ccorr_rampc = 0;
		private DepthCueing depthCueing;
		private AmbientOcclusion ambientOcclusion;
//		private bool toggle_NA_SWITCH = false;
//		private	bool toggle_NA_MEASURE = false;
//		private bool toggle_MESHCOMBINE = false;
		private bool _toggle_NA_SWITCH = false;
		public bool toggle_NA_SWITCH {
			get { return _toggle_NA_SWITCH; }
			set { _toggle_NA_SWITCH = this.ProcessPropertyChanged("toggle_NA_SWITCH", _toggle_NA_SWITCH, value); }
		}
		private bool _toggle_NA_MEASURE = false;

		public bool toggle_NA_MEASURE {
			get { return _toggle_NA_MEASURE; }
			set { _toggle_NA_MEASURE = this.ProcessPropertyChanged("toggle_NA_MEASURE", _toggle_NA_MEASURE, value); }
		}
		private bool _toggle_MESHCOMBINE = false;
		public bool toggle_MESHCOMBINE {
			get { return _toggle_MESHCOMBINE; }
			set { _toggle_MESHCOMBINE = this.ProcessPropertyChanged("toggle_MESHCOMBINE", _toggle_MESHCOMBINE, value); }
		}
		public bool showGrayColor = false;
		public bool showSurfaceButton = false;
//		public bool showSurface = false;
//		public bool showVolumetricDepth = false;
//		public bool electroIsoSurfaceTransparency = false;
		private bool _showSurface = false;
		public bool showSurface {
			get { return _showSurface; }
			set { _showSurface = this.ProcessPropertyChanged("showSurface", _showSurface, value); }
		}
		private bool _showVolumetricDepth = false;
		public bool showVolumetricDepth {
			get { return _showVolumetricDepth; }
			set { _showVolumetricDepth = this.ProcessPropertyChanged("showVolumetricDepth", _showVolumetricDepth, value); }
		}
		private bool _electroIsoSurfaceTransparency = false;
		public bool electroIsoSurfaceTransparency {
			get { return _electroIsoSurfaceTransparency; }
			set { _electroIsoSurfaceTransparency = this.ProcessPropertyChanged("electroIsoSurfaceTransparency", _electroIsoSurfaceTransparency, value); }
		}
		public ColorObject BondColor  = new ColorObject(Color.black);
		public ColorObject BondColorcheck  = new ColorObject(Color.black);
		public ColorObject RingColor  = new ColorObject(Color.black);
		public ColorObject OxySphereColor  = new ColorObject(Color.red);
		public ColorObject OxySphereColorCheck  = new ColorObject(Color.red);
		public ColorObject RingColorcheck  = new ColorObject(Color.black);

		/// <summary>
		/// Sets the title of the current window.
		/// The FlexibleSpace() function around the Label is here for centering.
		/// </summary>
		/// <param name='s'>
		/// The title you wish to set for the window. String.
		/// </param>
		public void SetTitle(string s) {
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(s);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		
		/// <summary>
		/// This is a somewhat odd but very convenient function. When called by a function that defines
		/// the contents of a window, it creates a loosely centered label from its string argument,
		/// that basically acts as a title.
		/// It also creates a little 'close' button aligned to the right, and returns false if it was
		/// pressed, true otherwise.
		/// Therefore, the function both creates a title and determines whether the window must be
		/// kept open.
		/// </summary>
		/// <returns>
		/// A boolean value set to false if the 'close' button was pressed.
		/// </returns>
		/// <param name='s'>
		/// A string, the title to set for the current window.
		/// </param>
		public bool SetTitleExit(string s) {
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			bool keepOpen = true ;
			
			GUILayout.Label(s);
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(new GUIContent("X", "Close window")))
				keepOpen = false;
			
			GUILayout.EndHorizontal();
			
			return UnityClusterPackage.Node.CurrentNode.HasPermission(NodePermission.MenuControl) &&
				keepOpen;
		}
		
		
		
		/// <summary>
		/// Defines the style of atoms.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void AtomStyle (int a) {
			SetTitle("Choose Atom Style");
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("Cube", "Use cubes to represent atoms"))) {	
				ChangeRepresentation (UIData.AtomType.cube);
			}
			
			if (GUILayout.Button (new GUIContent ("Sphere", "Use triangulated spheres to represent atoms"))) {
				ChangeRepresentation (UIData.AtomType.sphere);
			}
			GUILayout.EndHorizontal ();			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Hyperball", "Use the HyperBalls shader to render atoms"))) {
				ChangeRepresentation (UIData.AtomType.hyperball);
			}			
	
			if (GUILayout.Button (new GUIContent ("Particle", "Use the ParticleBall shader to represent atoms"))) {
				ChangeRepresentation (UIData.AtomType.particleball);
			}						
			GUILayout.EndHorizontal ();
				
			// Those hidden features aren't working at all
/*			if (UIData.Instance.openAllMenu) { 
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Raycasting", "Use raycasting to represent atoms"))) {
					UIData.Instance.resetDisplay = true;
					UIData.Instance.isCubeToSphere = false;
					UIData.Instance.isSphereToCube = true;
					UIData.Instance.atomtype = UIData.AtomType.raycasting;
					Debug.Log ("UIData.Instance.resetDisplay:" + UIData.Instance.resetDisplay);
					Debug.Log ("UIData.Instance.isCubeToSphere:" + UIData.Instance.isCubeToSphere);
					Debug.Log ("UIData.Instance.isSphereToCube:" + UIData.Instance.isSphereToCube);
					GUIMoleculeController.Instance.showAtomType = false;
					GUIMoleculeController.Instance.toggle_NA_HIDE = false;
				}
				GUILayout.EndHorizontal ();
			}
			
			if (UIData.Instance.openAllMenu) {
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("RayCasting Sprite", "Use RayCasting Sprites to represent atoms"))) {
					UIData.Instance.resetDisplay = true;
					UIData.Instance.isSphereToCube = true;
					UIData.Instance.isCubeToSphere = false;
					UIData.Instance.atomtype = UIData.AtomType.rcsprite;
					Debug.Log ("UIData.Instance.resetDisplay:" + UIData.Instance.resetDisplay);
					Debug.Log ("UIData.Instance.isCubeToSphere:" + UIData.Instance.isCubeToSphere);
					Debug.Log ("UIData.Instance.isSphereToCube:" + UIData.Instance.isSphereToCube);
					GUIMoleculeController.Instance.showAtomType = false;
					GUIMoleculeController.Instance.toggle_NA_HIDE = false;
				}
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Multi-Hyperball", "Use Multi-Hyperballs rendering to represent atoms"))) {
					UIData.Instance.resetDisplay = true;
					UIData.Instance.isSphereToCube = true;
					UIData.Instance.isCubeToSphere = false;
					UIData.Instance.atomtype = UIData.AtomType.multihyperball;
					Debug.Log ("UIData.Instance.resetDisplay:" + UIData.Instance.resetDisplay);
					Debug.Log ("UIData.Instance.isCubeToSphere:" + UIData.Instance.isCubeToSphere);
					Debug.Log ("UIData.Instance.isSphereToCube:" + UIData.Instance.isSphereToCube);
					GUIMoleculeController.Instance.showAtomType = false;
					GUIMoleculeController.Instance.toggle_NA_HIDE = false;
				}				
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("CombineMesh HyperBall", "Use the CombineMesh HyperBall shader to represent atoms"))) {
					UIData.Instance.resetDisplay = true;
					UIData.Instance.isSphereToCube = true;
					UIData.Instance.isCubeToSphere = false;
					UIData.Instance.atomtype = UIData.AtomType.combinemeshball;
					Debug.Log ("UIData.Instance.resetDisplay:" + UIData.Instance.resetDisplay);
					Debug.Log ("UIData.Instance.isCubeToSphere:" + UIData.Instance.isCubeToSphere);
					Debug.Log ("UIData.Instance.isSphereToCube:" + UIData.Instance.isSphereToCube);
					GUIMoleculeController.Instance.showAtomType = false;
					GUIMoleculeController.Instance.toggle_NA_HIDE = false;
				}						
				GUILayout.EndHorizontal ();
			}
			
			if (UIData.Instance.openAllMenu) {			
			
				GUILayout.BeginHorizontal ();
				
				GUILayout.Label ("Billboard", GUILayout.MaxWidth (50));
				
				UIData.Instance.toggleClip = GUILayout.Toggle (UIData.Instance.toggleClip, new GUIContent ("Clip", "Toggle the Clip plane"));
				UIData.Instance.togglePlane = !UIData.Instance.toggleClip;
				
				UIData.Instance.togglePlane = GUILayout.Toggle (UIData.Instance.togglePlane, new GUIContent ("Plane", "Toggle the Cut plane"));
				UIData.Instance.toggleClip = !UIData.Instance.togglePlane;
				
				GUILayout.EndHorizontal ();
	
				toggle_MESHCOMBINE = GUILayout.Toggle (toggle_MESHCOMBINE, new GUIContent ("MESHCOMBINE", "Toggle the mesh combination"));
		
				if (!toggle_MESHCOMBINE) {
					UIData.Instance.meshcombine = false;
					UIData.Instance.resetMeshcombine = true;			
				} else if (toggle_MESHCOMBINE) { 
					UIData.Instance.meshcombine = true;
					UIData.Instance.resetMeshcombine = true;
				}
			}
*/
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // end of AtomStyle
		
		
		
		//Sugar Menu
		public void SugarM (int a){
			GUIMoleculeController.Instance.showSugarChainMenu = SetTitleExit("Sugar");

			/*************************************************/
			// Luiz:
			GUILayout.BeginHorizontal();
			toggle_RING_BLENDING = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(toggle_RING_BLENDING,
				new GUIContent("Enable RingBlending", "enable RingBlending visualisation"));
			GUILayout.EndHorizontal();
			/*************************************************/

			//------- Twister
			/*************************************************/
			// Luiz:
			GUILayout.BeginHorizontal();
			toggle_TWISTER = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(toggle_TWISTER,
			                                                                  new GUIContent("Enable SugarRibbons", "Switch between all-atoms and SugarRibbons representation"));					
			GUILayout.EndHorizontal();
			/*************************************************/

			/*************************************************/
			// Luiz:
			GUILayout.BeginHorizontal();
			toggle_HIDE_HYDROGEN = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(toggle_HIDE_HYDROGEN,
			                                                                     new GUIContent("Hide Hydrogens", "hide hydrogens atoms"));
			GUILayout.EndHorizontal();
			/*************************************************/

			GUILayout.BeginHorizontal();
			GUILayout.Label(">> Hiding atoms");
			GUILayout.EndHorizontal();

			/*************************************************/
			// Luiz:
			GUILayout.BeginHorizontal();
			toggle_SHOW_HB_W_SR = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(toggle_SHOW_HB_W_SR,
			                                                                     new GUIContent("Sugar", "Hide sugar atoms"));

			toggle_SHOW_HB_NOT_SUGAR = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(toggle_SHOW_HB_NOT_SUGAR,
			                                                                    new GUIContent("Non Sugar", "Hide Non sugar Atoms"));
			GUILayout.EndHorizontal();			
			/*************************************************/

			/*************************************************/
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Tune Menu"))) {
				GUIMoleculeController.Instance.showSugarRibbonsTuneMenu = !GUIMoleculeController.Instance.showSugarRibbonsTuneMenu;
			}
			
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			/*************************************************/

			// Bugs otherwise.
			/*
			if(!UIData.Instance.hasMoleculeDisplay) {
				GUIMoleculeController.Instance.showSecStructMenu = false;
				return;
			}
*/
			GUILayout.FlexibleSpace();
			GUI.DragWindow();
		}


		public void SugarRibbonsTune(int a){
			GUIMoleculeController.Instance.showSugarRibbonsTuneMenu = SetTitleExit("Tune Menu");
			if (!GUIMoleculeController.Instance.showSugarChainMenu)
				GUIMoleculeController.Instance.showSugarRibbonsTuneMenu = false;

			int labelWidth = (int) (0.2f * Rectangles.SugarRibbonsTuneWidth);
			int sliderWidth = (int) (0.73f * Rectangles.SugarRibbonsTuneWidth);

			/*************************************************/
			GUILayout.BeginHorizontal();
			bool oxyToggled = GUIMoleculeController.Instance.toggle_OXYGEN; //to avoid to create all sphere to each frames
			GUIMoleculeController.Instance.toggle_OXYGEN = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(GUIMoleculeController.Instance.toggle_OXYGEN,
			                                                              new GUIContent("Show Oxygens", "show ring oxygens with a red sphere"));
			
			if (toggle_TWISTER && GUIMoleculeController.Instance.toggle_OXYGEN && !oxyToggled) {
				SR.ShowOxySphere ();		
				oxyToggled = true;
			} else if (toggle_RING_BLENDING && GUIMoleculeController.Instance.toggle_OXYGEN && !oxyToggled) {
				ringblending.ShowOxySphere();
				oxyToggled = true;
			}else{
				if (oxyToggled && !GUIMoleculeController.Instance.toggle_OXYGEN){
					GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
					foreach(GameObject OxySphere in OxySpheres)
						GameObject.Destroy(OxySphere);
				}
			}
			
			GUIMoleculeController.Instance.toggle_SUGAR_ONLY = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(GUIMoleculeController.Instance.toggle_SUGAR_ONLY,
			                                                                  new GUIContent("Sugar Only?", "use only sugar for RingBlending and SugarRibbons"));
			
			GUILayout.EndHorizontal();
			/*************************************************/
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Change Coloration"))) {
				GUIMoleculeController.Instance.showColorTuneMenu = !GUIMoleculeController.Instance.showColorTuneMenu;
			}
			
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			
				/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Oxygen Sphere Size");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			OxySphereSize = GUIMoleculeController.Instance.LabelSlider(OxySphereSize, 0.01f, 2f,
			                            OxySphereSize.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			if (OxySphereSizeCheck!=OxySphereSize){
				OxySphereSizeCheck=OxySphereSize;
				SR.OXYSPHERESIZE =  OxySphereSize;
				GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
				foreach(GameObject OxySphere in OxySpheres)
					OxySphere.transform.localScale = new Vector3(OxySphereSize, OxySphereSize, OxySphereSize);
			}


			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Ribbons Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			RibbonsThickness = GUIMoleculeController.Instance.LabelSlider(RibbonsThickness, 0.02f, 2f,
			                               RibbonsThickness.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Inner Ring Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			thickness_Little = GUIMoleculeController.Instance.LabelSlider(thickness_Little, 0f, 3f,
			                               thickness_Little.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Outer Ring Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			thickness_BIG = GUIMoleculeController.Instance.LabelSlider(thickness_BIG, 0.00f, 3f,
			                            thickness_BIG.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Pyranose (6) : C1,C4 Bond Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			thickness_bond_6_C1_C4 = GUIMoleculeController.Instance.LabelSlider(thickness_bond_6_C1_C4, 0.0f, 1f,
			                                     thickness_bond_6_C1_C4.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Pyranose (6) : Other Bond Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			thickness_6_other = GUIMoleculeController.Instance.LabelSlider(thickness_6_other, 0.01f, 0.3f,
			                                thickness_6_other.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Furanose (5) : Bond Thickness");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			thickness_bond_5 = GUIMoleculeController.Instance.LabelSlider(thickness_bond_5, 0.01f, 0.3f,
			                               thickness_bond_5.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			/*************************************************/

			/*************************************************/
			GUI.enabled = toggle_TWISTER;
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Apply changes"))) {
				// Destroying the ribbons
				ResetSugarRibbons();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Reset parameters"))) {
				// Destroying the ribbons
				ResetDefaultParametersSugarRibbons();
			}
			GUILayout.EndHorizontal();
			/*************************************************/

			GUILayout.FlexibleSpace();
			GUI.DragWindow();
		}

		//FOR SUGAR RIBBONS ONLY
		public void ColorTuneMenu(int a){
			GUIMoleculeController.Instance.showColorTuneMenu = SetTitleExit("ColorTune Menu");
			if (!GUIMoleculeController.Instance.showSugarChainMenu)
				GUIMoleculeController.Instance.showColorTuneMenu = false;
			int labelWidth = (int) (0.2f * Rectangles.ColorTuneWidth);
			int sliderWidth = (int) (0.73f * Rectangles.ColorTuneWidth);

			/*************************************************/

			GUILayout.BeginHorizontal();
			GUILayout.Label("OXYGEN SPHERE");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Sugar", "Coloration by sugar Type"))) {
				GUIMoleculeController.Instance.oxyToggled = true;
				GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
				foreach(GameObject OxySphere in OxySpheres)
					GameObject.Destroy(OxySphere);

				SR.ShowOxySphere(1);
				GUIMoleculeController.Instance.toggle_OXYGEN=true;
			}
			if (GUILayout.Button (new GUIContent ("Chain", "Coloration by Chain"))) {
				GUIMoleculeController.Instance.oxyToggled = true;
				GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
				foreach(GameObject OxySphere in OxySpheres)
					GameObject.Destroy(OxySphere);
				
				SR.ShowOxySphere(2);
				GUIMoleculeController.Instance.toggle_OXYGEN=true;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Custom", "Choose a custom Color"))) {
				if (GUIMoleculeController.Instance.m_colorPicker != null)
					GUIMoleculeController.Instance.m_colorPicker = null;
				
				GUIMoleculeController.Instance.m_colorPicker = new ColorPicker(Rectangles.colorPickerRect,	OxySphereColor, null, "All", "All", "Color Picket");
			}
			GUILayout.EndHorizontal ();
			if (OxySphereColorCheck.color != OxySphereColor.color){
				OxySphereColorCheck.color = OxySphereColor.color;
				
				GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
				if (OxySpheres.Length>0){
					foreach(GameObject OxySphere in OxySpheres)
						OxySphere.GetComponent<Renderer>().material.color = OxySphereColor.color;
				}else{
					SR.ShowOxySphere();
					GUIMoleculeController.Instance.toggle_OXYGEN=true;
				}
				
			}
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("RINGS");
			GUILayout.EndHorizontal();


			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("Sugar", "Coloration by sugar Type"))) {
				ColorationModeRing=0;
				//ResetSugarRibbons();
				SR.updateColor("SugarRibbons_RING_BIG", ColorationModeRing);
			}

			if (GUILayout.Button (new GUIContent ("Chain", "Coloration by Chain"))) {
				ColorationModeRing=1;
				//ResetSugarRibbons();
				SR.updateColor("SugarRibbons_RING_BIG", ColorationModeRing);
			}
			GUILayout.EndHorizontal();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Custom Color", "Choose a custom Color"))) {
				if (GUIMoleculeController.Instance.m_colorPicker != null)
					GUIMoleculeController.Instance.m_colorPicker = null;
				
				GUIMoleculeController.Instance.m_colorPicker = new ColorPicker(Rectangles.colorPickerRect,	RingColor, null, "All", "All", "Color Picket");

			}
			GUILayout.EndHorizontal ();

			if (RingColorcheck.color != RingColor.color){
				RingColorcheck.color = RingColor.color;
				ColorationModeRing=2;

				SR.updateColor("SugarRibbons_RING_BIG", RingColor);
			}
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Lighter Color Factor");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			lighter_color_factor_ring = GUIMoleculeController.Instance.LabelSlider(lighter_color_factor_ring, -1f, 1f,
			                                       lighter_color_factor_ring.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();

			if (lighter_color_factor_ring_check != lighter_color_factor_ring){
				lighter_color_factor_ring_check=lighter_color_factor_ring;
				SR.LIGHTER_COLOR_FACTOR_RING = lighter_color_factor_ring;

				if (ColorationModeRing==2)
					SR.updateColor("SugarRibbons_RING_BIG", RingColor);
				else
					SR.updateColor("SugarRibbons_RING_BIG", ColorationModeRing);
			}

			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("BONDS");
			GUILayout.EndHorizontal();

			
			/*************************************************/
			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("Sugar", "Coloration by sugar Type"))) {
				ColorationModeBond=0;
				SR.updateColor("SugarRibbons_BOND", ColorationModeBond); //TODO find a way to retreive vertex coloration with side sugar type.
				//ResetSugarRibbons();
			}
			
			if (GUILayout.Button (new GUIContent ("Chain", "Coloration by Chain"))) {
				ColorationModeBond=1;
				SR.updateColor("SugarRibbons_BOND", ColorationModeBond);
			}
			GUILayout.EndHorizontal ();
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Custom Color", "Choose a custom Color"))) {
				if (GUIMoleculeController.Instance.m_colorPicker != null)
					GUIMoleculeController.Instance.m_colorPicker = null;
				
				GUIMoleculeController.Instance.m_colorPicker = new ColorPicker(Rectangles.colorPickerRect,	BondColor, null, "All", "All", "Color Picket");
			}
			GUILayout.EndHorizontal ();
			if (BondColorcheck.color != BondColor.color){
				BondColorcheck.color = BondColor.color;

				ColorationModeBond=2;
				SR.updateColor("SugarRibbons_BOND", BondColor);
			}
			/*************************************************/
			/*************************************************/
			GUILayout.BeginHorizontal();
			GUILayout.Label("Lighter Color Factor");
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			lighter_color_factor_bond = GUIMoleculeController.Instance.LabelSlider(lighter_color_factor_bond, -1f, 1f,
			                                        lighter_color_factor_bond.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();

			if (lighter_color_factor_bond_check != lighter_color_factor_bond){
				lighter_color_factor_bond_check=lighter_color_factor_bond;
				SR.LIGHTER_COLOR_FACTOR_BOND = lighter_color_factor_bond;
				if(ColorationModeBond==2)
					SR.updateColor("SugarRibbons_BOND", BondColor);
				else
					SR.updateColor("SugarRibbons_BOND", ColorationModeBond);


			}
			
			/*************************************************/
			
			GUILayout.FlexibleSpace();
			GUI.DragWindow();
		}

		public void ResetSugarRibbons(){
			GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;

			GameObject[] objs; 
			objs= GameObject.FindGameObjectsWithTag("SugarRibbons_RING_BIG");
			foreach(GameObject obj in objs)
				GameObject.Destroy(obj);

			objs = GameObject.FindGameObjectsWithTag("SugarRibbons_RING_little");
			foreach(GameObject obj in objs)
				GameObject.Destroy(obj);
			objs = GameObject.FindGameObjectsWithTag("SugarRibbons_BOND");
			foreach(GameObject obj in objs)
				GameObject.Destroy(obj);
			//We flush all the previous list.
			SR.cleanup();
			// Recreating them
			SR.createSugarRibs(RibbonsThickness, GUIMoleculeController.Instance.toggle_SUGAR_ONLY, thickness_Little, thickness_BIG, 
			                   thickness_bond_6_C1_C4, thickness_6_other, thickness_bond_5, lighter_color_factor_ring, lighter_color_factor_bond, 
			                   ColorationModeRing, ColorationModeBond, BondColor, RingColor, OxySphereSize, OxySphereColor);
			GUIMoleculeController.Instance.toggle_NA_HIDE = true;


			if (GUIMoleculeController.Instance.toggle_OXYGEN){
				GameObject[] OxySpheres = GameObject.FindGameObjectsWithTag("OxySphere");
				foreach(GameObject OxySphere in OxySpheres)
					GameObject.Destroy(OxySphere);
				SR.ShowOxySphere ();		

			}
		}

		public void show_HyperBalls_Sugar(bool show){
			int i=0;
			for (i=0; i<HBallManager.hballs.Length; i++){
				int atom_number =(int) HBallManager.hballs[i].GetComponent<BallUpdate>().number;
				if (MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number])){
					HBallManager.hballs[i].GetComponent<Renderer>().enabled=show;
				}
				if (i<HStickManager.sticks.Count){ 
					int atom_number1=(int)HStickManager.sticks[i].atompointer1.GetComponent<BallUpdate>().number;
					int atom_number2=(int)HStickManager.sticks[i].atompointer2.GetComponent<BallUpdate>().number;

					if ((MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number1]))||
					    (MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number2])))
						HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				}
			}
			//If we didn't finish to check the bond list (more bond than atoms)
			//we check the end of the list.
			while(i<HStickManager.sticks.Count){
				int atom_number1=(int)HStickManager.sticks[i].atompointer1.GetComponent<BallUpdate>().number;
				int atom_number2=(int)HStickManager.sticks[i].atompointer2.GetComponent<BallUpdate>().number;
				if ((MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number1]))||
				    (MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number2])))
					HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				i++;
			}
		}

		/*This fonction is made to hide hyperballs which are not Sugar Atoms*/
		public void Hide_No_Sugar_Hiperballs(bool show){
			int i=0;
			for (i=0; i<HBallManager.hballs.Length; i++){
				int atom_number =(int) HBallManager.hballs[i].GetComponent<BallUpdate>().number;
				if (!MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number])){
					HBallManager.hballs[i].GetComponent<Renderer>().enabled=show;
				}
				if (i<HStickManager.sticks.Count){ 
					int atom_number1=(int)HStickManager.sticks[i].atompointer1.GetComponent<BallUpdate>().number;
					int atom_number2=(int)HStickManager.sticks[i].atompointer2.GetComponent<BallUpdate>().number;
					
					if ((!MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number1]))||
					    (!MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number2])))
						HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				}
			}
			//If we didn't finish to check the bond list (more bond than atoms)
			//we check the end of the list.
			while(i<HStickManager.sticks.Count){
				int atom_number1=(int)HStickManager.sticks[i].atompointer1.GetComponent<BallUpdate>().number;
				int atom_number2=(int)HStickManager.sticks[i].atompointer2.GetComponent<BallUpdate>().number;
				if ((!MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number1]))||
				    (!MoleculeModel.sugarResname.Contains(MoleculeModel.atomsResnamelist[atom_number2])))
					HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				i++;
			}
		}

		public void showHydrogens(bool show){
			int i=0;
			for (i=0; i<HBallManager.hballs.Length; i++){
				if (HBallManager.hballs[i].tag=="H"){
					HBallManager.hballs[i].GetComponent<Renderer>().enabled=show;
				}
				// We want to check atoms and bond in one loop. 
				//so we check if we not over the size of the bond list
				if (i<HStickManager.sticks.Count){ 
					if ((HStickManager.sticks[i].atompointer2.tag == "H") ||
					    (HStickManager.sticks[i].atompointer1.tag == "H"))
						HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				}
			}
			//If we didn't finish to check the bond list (more bond than atoms)
			//we check the end of the list.
			while(i<HStickManager.sticks.Count){
				if ((HStickManager.sticks[i].atompointer2.tag == "H") ||
				    (HStickManager.sticks[i].atompointer1.tag == "H"))
					HStickManager.sticks[i].GetComponent<Renderer>().enabled=show;
				i++;
			}
		}


		
		public void ResetDefaultParametersSugarRibbons(){

			RibbonsThickness=0.15f;
			thickness_Little=1.8f;
			thickness_BIG=1f;
			thickness_bond_6_C1_C4=0.2f;
			thickness_6_other=0.16f;
			thickness_bond_5=0.2f;
			lighter_color_factor_ring=0.35f;
			lighter_color_factor_bond=0.35f;
			ColorationModeRing=0;
			ColorationModeBond=0;
			OxySphereSize=1f;
			OxySphereColor.color=Color.red;
			lighter_color_factor_bond=0.35f;
			lighter_color_factor_ring=0.35f;

			ResetSugarRibbons();
		}

		/// <summary>
		/// Defines the bond type selection menu window, which is called from the appearance menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Bond (int a)	{
			SetTitle("Bond Style");
			
//			if(UIData.Instance.atomtype==UIData.AtomType.particleball&&!UIData.Instance.openAllMenu)GUI.enabled=false;
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();

			if (GUILayout.Button(new GUIContent("Cube", "Use Cubes to represent bonds"), GUILayout.Width(Rectangles.atomButtonWidth))) {
				if (TrajectoryData.Instance.IsLoaded) {
					UIData.Instance.SetError(true, "Cube bonds not available when using trajectories");
				} else {
					ChangeBond(UIData.BondType.cube);
				}
			}

			if (GUILayout.Button (new GUIContent ("Line", "Use the Line renderer to represent bonds"), GUILayout.Width(Rectangles.atomButtonWidth))) {
				ChangeBond (UIData.BondType.line);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button (new GUIContent ("HyperStick", "Use the HyperStick shader to represent bonds"), GUILayout.Width(Rectangles.atomButtonWidth))) {	
				ChangeBond (UIData.BondType.hyperstick);
			}

			if (GUILayout.Button (new GUIContent ("No Stick", "Do not render any bonds"), GUILayout.Width(Rectangles.atomButtonWidth))) {
				ChangeBond (UIData.BondType.nobond);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();
			
			// Those hidden features aren't working at all		
/*			if (UIData.Instance.openAllMenu) {	
				GUILayout.BeginHorizontal ();

				if (GUILayout.Button (new GUIContent ("Tube Stick", "Use the Tube Stick renderer to represent bonds"))) {
					UIData.Instance.resetBondDisplay = true;
					UIData.Instance.bondtype = UIData.BondType.tubestick;
					GUIMoleculeController.Instance.showBondType = false;
				}

				GUILayout.EndHorizontal ();
			
				GUILayout.BeginHorizontal ();

				if (GUILayout.Button (new GUIContent ("Billboard HyperStick", "Use the Billboard HyperStick shader to represent bonds"))) {
					UIData.Instance.resetBondDisplay = true;
					UIData.Instance.bondtype = UIData.BondType.bbhyperstick;
				}

				GUILayout.EndHorizontal ();
			
				GUILayout.BeginHorizontal ();

				if (GUILayout.Button (new GUIContent ("Particle Stick", "Use the Particle Stick shader to represent bonds"))) {
					UIData.Instance.resetBondDisplay = true;
					UIData.Instance.bondtype = UIData.BondType.particlestick;
					GUIMoleculeController.Instance.showBondType = false;
				}

				GUILayout.EndHorizontal ();
			}
*/
			GUI.enabled = true;
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // end of Bond

		
		/// <summary>
		/// Defines the Metaphor menu window, which is launched by the Metaphor button in the  Hperball Style window
		///
		/// Luiz: Good lord, the unprofessiolism of this code was raping my college degree with its infinite repetition of the same code.
		/// Luckly, the great hero here made it far better than before.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Metaphor (int a) {
			GUIMoleculeController.Instance.showMetaphorType = SetTitleExit("Metaphor");
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("CPK", "CPK representation as balls and sticks"))) {
				GUIMoleculeController.Instance.ChangeMetaphor(.2f, .0001f, .3f);
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();			
			if (GUILayout.Button (new GUIContent ("Licorice", "Licorice representation of the molecule"))) {
				GUIMoleculeController.Instance.ChangeMetaphor(.1f, .0001f, 1f);
			}						
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			
			if (GUILayout.Button (new GUIContent ("VdW", "van der Waals representation as spacefilling spheres"))) {
				GUIMoleculeController.Instance.ChangeMetaphor(1f, .8f, 1f);
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();			
			if (GUILayout.Button (new GUIContent ("Smooth", "Smooth HyperBalls metaphor representation"))) {
				GUIMoleculeController.Instance.ChangeMetaphor(.35f, .4f, 1f);
			}						
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();			
			if (GUILayout.Button (new GUIContent ("SmoothLink", "SmoothLink HyperBalls representation"))) {
				GUIMoleculeController.Instance.ChangeMetaphor(.4f, .5f, 1f);
			}						
			GUILayout.EndHorizontal ();

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of Metaphor
		
		
		
		/// <summary>
		/// Defines the electrostatic menu window, which is opened from the main menu window.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Electrostatics (int a) {
			#if UNITY_WEBPLAYER
			GUI.enabled = false;
			#endif
			GUIMoleculeController.Instance.showElectrostaticsMenu = SetTitleExit("Electrostatics");
			GUILayout.BeginHorizontal ();
				electroIsoSurfaceTransparency = GUILayout.Toggle(electroIsoSurfaceTransparency, new GUIContent("Transparency", "Toggle transparent isosurfaces."));
			GUILayout.EndHorizontal ();		
			
			GUILayout.BeginHorizontal ();
			int sliderWidth = (int) (Rectangles.electroMenuWidth * 0.60f);
			GUIMoleculeController.Instance.generateThresholdDx_neg = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.generateThresholdDx_neg, -10f, 0f, 
							"T: " + Mathf.Round (GUIMoleculeController.Instance.generateThresholdDx_neg * 10f) / 10f, "Ramp value used for surface generation",GUI.enabled , sliderWidth, 1);
			GUILayout.EndHorizontal ();
			
			if (!MoleculeModel.dxFileExists)
				GUI.enabled = false ;
			
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Load Neg.", "Read an OpenDx format electrostatic field and generate a surface"))) {
				MoleculeModel.surfaceFileExists = true;
				
				GUIMoleculeController.Instance.readdx.ReadFile(GUIDisplay.Instance.file_base_name+".dx",MoleculeModel.Offset);
				GUIMoleculeController.Instance.dxRead = true;
				
				string tag = "Elect_iso_negative";
				GUIMoleculeController.Instance.electroIsoNegativeInitialized = true;
				GUIMoleculeController.Instance.showElectroIsoNegative = true;

				GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
				foreach(GameObject iso in IsoSurfaces)
					Object.Destroy(iso);
				GUIMoleculeController.Instance.readdx.isoSurface (GUIMoleculeController.Instance.generateThresholdDx_neg,Color.red,tag, electroIsoSurfaceTransparency);
			}
			
			GUI.enabled = true ;
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUIMoleculeController.Instance.generateThresholdDx_pos = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.generateThresholdDx_pos, 0f, 10f, 
							"T: " + Mathf.Round (GUIMoleculeController.Instance.generateThresholdDx_pos * 10f) / 10f, "Ramp value used for surface generation", GUI.enabled, sliderWidth, 1);
			GUILayout.EndHorizontal ();
			
			if (!MoleculeModel.dxFileExists)
				GUI.enabled = false ;
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Load Pos.", "Read an OpenDx format electrostatic field and generate a surface"))) {
				MoleculeModel.surfaceFileExists = true;	
				GUIMoleculeController.Instance.readdx.ReadFile(GUIDisplay.Instance.file_base_name+".dx",MoleculeModel.Offset);
				GUIMoleculeController.Instance.dxRead = true;
				string tag = "Elect_iso_positive";
				GUIMoleculeController.Instance.showElectroIsoPositive = true;
				GUIMoleculeController.Instance.electroIsoPositiveInitialized = true;

				GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
				foreach(GameObject iso in IsoSurfaces)
					Object.Destroy(iso);
				GUIMoleculeController.Instance.readdx.isoSurface (GUIMoleculeController.Instance.generateThresholdDx_pos,Color.blue,tag, electroIsoSurfaceTransparency);
			}
			
			GUI.enabled = true ;
			GUILayout.EndHorizontal ();

			if (GUIMoleculeController.Instance.dxRead && GUIMoleculeController.Instance.electroIsoNegativeInitialized)
				GUI.enabled = true;
			else 
				GUI.enabled = false;
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("Toggle Neg.", "Toggles negative iso-surface  from visible to hidden and vice versa"))) {
				string tag = "Elect_iso_negative";
				if (GUIMoleculeController.Instance.showElectroIsoNegative) {
					showSurface = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
					GUIMoleculeController.Instance.showElectroIsoNegative = false;
					GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
					foreach(GameObject iso in IsoSurfaces)
						iso.GetComponent<Renderer>().enabled = false;
				} else {
					GUIMoleculeController.Instance.showElectroIsoNegative = true;
					GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
					foreach(GameObject iso in IsoSurfaces)
						iso.GetComponent<Renderer>().enabled = true;
				}				
			
			}
			GUILayout.EndHorizontal ();
			
			GUI.enabled = (GUIMoleculeController.Instance.dxRead && GUIMoleculeController.Instance.electroIsoPositiveInitialized);
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("Toggle Pos.", "Toggles positive iso-surface from visible to hidden and vice versa"))) {
				string tag = "Elect_iso_positive";
				if (GUIMoleculeController.Instance.showElectroIsoPositive) {
					showSurface = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
					GUIMoleculeController.Instance.showElectroIsoPositive = false;
					GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
					foreach(GameObject iso in IsoSurfaces)
						iso.GetComponent<Renderer>().enabled = false;
				} else {
					GUIMoleculeController.Instance.showElectroIsoPositive = true;
					GameObject[] IsoSurfaces = GameObject.FindGameObjectsWithTag(tag);
					foreach(GameObject iso in IsoSurfaces)
						iso.GetComponent<Renderer>().enabled = true;
				}
			}
			GUILayout.EndHorizontal();

			#if UNITY_WEBPLAYER
			if(!MoleculeModel.fieldLineFileExists)
				GUI.enabled = false;
			else
				GUI.enabled = true;
			#else
			GUI.enabled = true;
			#endif
			
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Volumetric Fields", "Toggles volumetric rendering of electrostatic fields"))) {
				GUIMoleculeController.Instance.showVolumetricFields = !GUIMoleculeController.Instance.showVolumetricFields;
				GUIMoleculeController.Instance.readdx.ReadFile(GUIDisplay.Instance.file_base_name+".dx",MoleculeModel.Offset);
				GUIMoleculeController.Instance.dxRead = true;
				GameObject volumObj;
				volumObj = GameObject.FindGameObjectWithTag("Volumetric");
				VolumetricFields volumetricFields = null;
				
				if(GUIMoleculeController.Instance.showVolumetricFields) {
					volumObj.AddComponent<VolumetricFields>(); // adding the script
					volumetricFields = volumObj.GetComponent<VolumetricFields>();
					volumetricFields.Init();
				}
				else {
					volumetricFields = volumObj.GetComponent<VolumetricFields>();
					volumetricFields.Clear();
				}
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal ();
			if (!MoleculeModel.fieldLineFileExists)
				GUI.enabled = false ;
			if (GUILayout.Button (new GUIContent ("Field Lines", "Toggles animated field lines from visible to hidden and vice versa"))) {
				if (GUIMoleculeController.Instance.showFieldLines) {
					GUIMoleculeController.Instance.showFieldLines = false;
					GUIMoleculeController.Instance.m_colorPicker = null ;
					GameObject FieldLineManager = GameObject.Find ("FieldLineManager");
					FieldLineModel Line = FieldLineManager.transform.GetComponent<FieldLineModel> ();
					Line.killCurrentEffects ();
				} else {
					GUIMoleculeController.Instance.showFieldLines = true;
					if(GameObject.FindGameObjectsWithTag("FieldLineManager").Length == 0)
						FieldLineStyle.DisplayFieldLine ();
				}				
			}
			GUI.enabled = true ;
			GUILayout.EndHorizontal ();
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of Electrostatic
		
		
		/// <summary>
		/// Creates a new texture of the appropriate size for a button, unless it already exists.
		/// Used by AdvOptions.
		/// </summary>
		/// <returns>
		/// A Texture2D.
		/// </returns>
		private Texture2D MakeButtonTex(Texture2D tex) {
			if (tex)
				return(tex);
			else
				return (new Texture2D(20,10,TextureFormat.ARGB32,false));
		}
		
		
		
		/// <summary>
		/// Defines the advanced options menu window, which is opened from the main menu window.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void AdvOptions(int a){
			GUIMoleculeController.Instance.showAdvMenu = SetTitleExit("Advanced Options");
			
			GUILayout.BeginHorizontal();
			GUILayout.Label (new GUIContent ("GUI Scale: " + GUIDisplay.Instance.guiScale.ToString("0.00"), "Adjusts the scale of the GUI windows"), GUILayout.MinWidth ((int)(Rectangles.advOptWidth * 0.4f)));
			GUIDisplay.Instance.guiScale = GUILayout.HorizontalSlider (GUIDisplay.Instance.guiScale, 0.3f, 1.7f, GUILayout.Width (((int)(Rectangles.advOptWidth * 0.4f))));
			
			if (GUILayout.Button(new GUIContent("OK", "Apply new GUI Scale")))
				if(GUIDisplay.Instance.guiScale != GUIDisplay.Instance.oldGuiScale) {
					GUIDisplay.Instance.oldGuiScale = GUIDisplay.Instance.guiScale;
					Rectangles.Scale();
				}
			GUILayout.EndHorizontal();

			if (!UIData.Instance.hasMoleculeDisplay) {
				return;
			}
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(new GUIContent("Ortho/Persp", "Switches between orthographic and perspective camera"))){
				if(Camera.main.orthographic)
					Camera.main.orthographic = false ;
				else {
					Camera.main.orthographic = true ;
					Camera.main.orthographicSize = 20f ;
				}
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			orthoSize = GUIMoleculeController.Instance.LabelSlider(Camera.main.orthographicSize, minOrthoSize, maxOrthoSize, 
				"Camera Size", "This slider changes the size of the orthographic camera.", Camera.main.orthographic, 100, 20);
			GUI.enabled = true; // LabeLSlider can disable the entire GUI. I don't like that at all, but it's expected in some parts of the GUI.
			// Still needs changing, methinks. ---Alexandre
			GUILayout.EndHorizontal();
			
				Camera.main.orthographicSize = orthoSize ;
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(new GUIContent("Best Textures: " + (GUIMoleculeController.Instance.onlyBestTextures ? "On" : "Off"), 
												"This switches between the complete set of textures and a selection of the best ones"))){
				GUIMoleculeController.Instance.onlyBestTextures = !GUIMoleculeController.Instance.toggle_OXYGEN;
				if(GUIMoleculeController.Instance.onlyBestTextures)
					GUIMoleculeController.Instance.texture_set = 0;
				else
					GUIMoleculeController.Instance.texture_set = 5;
			}
			
			if (GUILayout.Button(new GUIContent("Depth Cueing", "Depth Cueing"))) {
				if (!GUIMoleculeController.Instance.dxRead) {
					GUIMoleculeController.Instance.readdx.ReadFile(GUIDisplay.Instance.file_base_name+".dx",MoleculeModel.Offset);
					GUIMoleculeController.Instance.dxRead = true;
				}
				if(DepthCueing.isEnabled && !DepthCueing.reset)
					depthCueing.Revert();
				else {
					depthCueing = new DepthCueing();
					depthCueing.Darken();
				}
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(new GUIContent("Volumetric Depth Cueing", "Volumetric Depth Cueing"))) {
				showVolumetricDepth = !showVolumetricDepth;
				
				if (!GUIMoleculeController.Instance.dxRead) {
					GUIMoleculeController.Instance.readdx.ReadFile(GUIDisplay.Instance.file_base_name+".dx",MoleculeModel.Offset);
					GUIMoleculeController.Instance.dxRead = true;
				}
				
				GameObject volumObj;
				volumObj = GameObject.FindGameObjectWithTag("Volumetric");
				VolumetricDepth volumetricDepth = null;
				
				if(showVolumetricDepth) {
					volumObj.AddComponent<VolumetricDepth>(); // adding the script
					volumetricDepth = volumObj.GetComponent<VolumetricDepth>();
					volumetricDepth.Init();
				}
				else {
					volumetricDepth = volumObj.GetComponent<VolumetricDepth>();
					volumetricDepth.Clear();
				}
			}
			
			if(GUILayout.Button(new GUIContent("Ambient Occlusion", "Ambient occlusion based on molecular density."))) {
				if(AmbientOcclusion.isEnabled && !AmbientOcclusion.reset)
					ambientOcclusion.Revert();
				else {
					ambientOcclusion = new AmbientOcclusion();
					ambientOcclusion.Occlude();
				}
			}
			GUILayout.EndHorizontal();

			// TODO : Make C-Alpha trace and his texture/color work again !
/*			//if((structType != "C-alpha trace"))
				//GUI.enabled = false ;
			
			// Creating textures to put on the buttons. Not created if they already exist.
			chainATex = MakeButtonTex(chainATex);
			chainBTex = MakeButtonTex(chainBTex);
			chainCTex = MakeButtonTex(chainCTex);
			chainDTex = MakeButtonTex(chainDTex);
			chainETex = MakeButtonTex(chainETex);
			chainFTex = MakeButtonTex(chainFTex);
			chainGTex = MakeButtonTex(chainGTex);
			chainHTex = MakeButtonTex(chainHTex);
			
			// Refreshing the color of each chain if needed
			if( (AtomModel.IsAlive()) && (!chainColorsInit) ) {
				chainAColor.color = AtomModel.GetChainColor("chainA");
				chainBColor.color = AtomModel.GetChainColor("chainB");
				chainCColor.color = AtomModel.GetChainColor("chainC");
				chainDColor.color = AtomModel.GetChainColor("chainD");
				chainEColor.color = AtomModel.GetChainColor("chainE");
				chainFColor.color = AtomModel.GetChainColor("chainF");
				chainGColor.color = AtomModel.GetChainColor("chainG");
				chainHColor.color = AtomModel.GetChainColor("chainH");
				
				chainColorsInit = true;
			}
			
			// Getting the current colors of each chain
			for(int i=0; i<200; i++) {
				if(!AtomModel.IsAlive()) {
					chainAColors[i] = Color.white;
					chainBColors[i] = Color.white;
					chainCColors[i] = Color.white;
					chainDColors[i] = Color.white;
					chainEColors[i] = Color.white;
					chainFColors[i] = Color.white;
					chainGColors[i] = Color.white;
					chainHColors[i] = Color.white;
				}
				else {
					chainAColors[i] = chainAColor.color;
					chainBColors[i] = chainBColor.color;
					chainCColors[i] = chainCColor.color;
					chainDColors[i] = chainDColor.color;
					chainEColors[i] = chainEColor.color;
					chainFColors[i] = chainFColor.color;
					chainGColors[i] = chainGColor.color;
					chainHColors[i] = chainHColor.color;
				}
			}
			
			
			// Actually setting the colors to the textures
			chainATex.SetPixels(chainAColors);
			chainATex.Apply(true);
			
			chainBTex.SetPixels(chainBColors);
			chainBTex.Apply(true);
			
			chainCTex.SetPixels(chainCColors);
			chainCTex.Apply(true);
			
			chainDTex.SetPixels(chainDColors);
			chainDTex.Apply(true);
			
			chainETex.SetPixels(chainEColors);
			chainETex.Apply(true);
			
			chainFTex.SetPixels(chainFColors);
			chainFTex.Apply(true);
			
			chainGTex.SetPixels(chainGColors);
			chainGTex.Apply(true);
			
			chainHTex.SetPixels(chainHColors);
			chainHTex.Apply(true);
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Carbon alpha trace chains colors");
			GUILayout.EndHorizontal();			
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain A color");
			if(GUILayout.Button(chainATex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainAColor, "Chain A Color", "chainA");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain B color");
			if(GUILayout.Button(chainBTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainBColor, "Chain B color", "chainB");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain C color");
			if(GUILayout.Button(chainCTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainCColor, "Chain C color", "chainC");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain D color");
			if(GUILayout.Button(chainDTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainDColor, "Chain D color", "chainD");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain E color");
			if(GUILayout.Button(chainETex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainEColor, "Chain E color", "chainE");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain F color");
			if(GUILayout.Button(chainFTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainFColor, "Chain F color", "chainF");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain G color");
			if(GUILayout.Button(chainGTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainGColor, "Chain G color", "chainG");
			GUILayout.EndHorizontal();
			
			// New Layout line
			GUILayout.BeginHorizontal();
			GUILayout.Label("Chain H color");
			if(GUILayout.Button(chainHTex,GUILayout.MinWidth(50),GUILayout.MinHeight(20)))
				GUIMoleculeController.Instance.CreateColorPicker(chainHColor, "Chain H color", "chainH");
			GUILayout.EndHorizontal();		
			
			// New Layout line
			GUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent("Apply", "Apply changes"))) {
				AtomModel.ChangeChainColor("chainA", chainAColor.color);
				AtomModel.ChangeChainColor("chainB", chainBColor.color);
				AtomModel.ChangeChainColor("chainC", chainCColor.color);
				AtomModel.ChangeChainColor("chainD", chainDColor.color);
				AtomModel.ChangeChainColor("chainE", chainEColor.color);
				AtomModel.ChangeChainColor("chainF", chainFColor.color);
				AtomModel.ChangeChainColor("chainG", chainGColor.color);
				AtomModel.ChangeChainColor("chainH", chainHColor.color);
				
				UIData.Instance.resetDisplay = true;
				//BallUpdate.resetColors = true;
			}
			GUILayout.EndHorizontal();
*/
			
			GUI.enabled = true;
			GUI.DragWindow();
		} // End of AdvOptions

		public void GuidedOptions(int a){
			//Debug.Log ("GUIDED ACTIVE");
			GUIMoleculeController.Instance.showGuidedMenu = SetTitleExit("Guided Navigation");
			
//			if(!SymmetryLoaded)
//			{
			if(!UIData.Instance.guided)
			{
				// Enter the origin of symmetry as an array of 3 floats
				GUILayout.BeginHorizontal();
				GUILayout.Label (new GUIContent ("Symmetry origin: ", "Enter here the origin 3D coordinates of the symmetry axis"), GUILayout.MinWidth ((int)(Rectangles.advOptWidth * 0.4f)));
				//try{
					SymmetryOriginX = GUILayout.TextField (SymmetryOriginX, 8);
					SymmetryOriginY = GUILayout.TextField (SymmetryOriginY, 8);
					SymmetryOriginZ = GUILayout.TextField (SymmetryOriginZ, 8);
				//} catch {}
				GUILayout.EndHorizontal ();
				// Enter the direction of symmetry as an array of 3 floats
				GUILayout.BeginHorizontal();
				GUILayout.Label (new GUIContent ("Symmetry direction: ", "Enter here the direction of the symmetry axis"), GUILayout.MinWidth ((int)(Rectangles.advOptWidth * 0.4f)));
				//try{
					SymmetryDirectionX = GUILayout.TextField (SymmetryDirectionX, 8);
					SymmetryDirectionY = GUILayout.TextField (SymmetryDirectionY, 8);
					SymmetryDirectionZ = GUILayout.TextField (SymmetryDirectionZ, 8);
	         	//} catch{}
				GUILayout.EndHorizontal ();
	
				// Submit the symmetry information to proceed to the atoms coordinates changes (only one time)
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Send", "submit the symmetrical information."))) {
					float OriginX = float.Parse(SymmetryOriginX);
					float OriginY = float.Parse(SymmetryOriginY);
					float OriginZ = float.Parse(SymmetryOriginZ);
					float DirectionX = float.Parse(SymmetryDirectionX);
					float DirectionY = float.Parse(SymmetryDirectionY);
					float DirectionZ = float.Parse(SymmetryDirectionZ);
					Reorient.LoadSymmetry (OriginX, OriginY, OriginZ, DirectionX, DirectionY, DirectionZ);
				}
				GUILayout.EndHorizontal ();
			}
			
			if(UIData.Instance.guided)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label (new GUIContent ("Target: ", "Enter here the 3D coordinates of the atom target"), GUILayout.MinWidth ((int)(Rectangles.advOptWidth * 0.4f)));
				//try{
				TargetX = GUILayout.TextField (TargetX, 8);
				TargetY = GUILayout.TextField (TargetY, 8);
				TargetZ = GUILayout.TextField (TargetZ, 8);
				//} catch {}
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal();
				GUILayout.Label (new GUIContent ("Camera distance: ", "Enter here the desired distance between the target and the camera"), GUILayout.MinWidth ((int)(Rectangles.advOptWidth * 0.4f)));
				//try{
				CameraDistance = GUILayout.TextField (CameraDistance, 8);
				//} catch {}
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Send", "compute the best point-of-view for the target coordinates."))) {
					float[] TargetCoords = new float[3];
					TargetCoords[0] = float.Parse(TargetX);
					TargetCoords[1] = float.Parse(TargetY);
					TargetCoords[2] = float.Parse(TargetZ);
					float Distance = float.Parse(CameraDistance);
					OptimalView.GetOptimalPosition(TargetCoords, Distance);
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				showOriginAxe = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(showOriginAxe,
				new GUIContent("Show symmetry axe and origin point", "Hide/Show symmetry axe and origin point"));
				if(showOriginAxe && !originThere){
					Reorient.CreateAxeAndOrigin();
					originThere = true;
				}else if(!showOriginAxe && originThere){
					GameObject[] Objs = GameObject.FindGameObjectsWithTag("Origin");
					foreach(GameObject obj in Objs)
						GameObject.Destroy (obj);
					originThere = false;
				}
				GUILayout.EndHorizontal ();
			}

			GUI.enabled = true;
			GUI.DragWindow();
		}

		/// <summary>
		/// Defines the Field Lines window, which is opened from the Electrostatic window.
		/// Opening this window should only be possible when a JSON file containing field lines has been loaded.
		/// </summary>
		/// <param name='a'>
		/// A.
		/// </param>
		public void FieldLines (int a) {
			
			if (GUILayout.Button (new GUIContent ("Energy/Field Color", "Choose color to represent potential energy or field lines"))) 
				GUIMoleculeController.Instance.CreateColorPicker(GUIMoleculeController.Instance.EnergyGrayColor, "Field Lines Color", null);

			if (GUILayout.Button (new GUIContent ("Color Gradient", "Display field lines with a color gradient")))
				GUIMoleculeController.Instance.fieldLineColorGradient = true;
			
			int sliderWidth = (int) (Rectangles.fieldLinesWidth * 0.8f);
			
			GUIMoleculeController.Instance.speed = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.speed, 0.001f, 1.0f, 
				"Speed  " + GUIMoleculeController.Instance.speed, "Determines field lines animation speed", true, sliderWidth, 1, true);
			GUIMoleculeController.Instance.density = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.density, 1.0f, 8.0f, 
				"Density  " + GUIMoleculeController.Instance.density, "Determines field lines density", true, sliderWidth, 1, true);
			GUIMoleculeController.Instance.linewidth = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.linewidth, 0.01f, 5.0f, 
				"Width  " + GUIMoleculeController.Instance.linewidth, "Determines field lines width", true, sliderWidth, 1, true);
			GUIMoleculeController.Instance.linelength = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.linelength, 0.8f, 0.1f, 
				"Length " + (1 - GUIMoleculeController.Instance.linelength), "Determines field lines length", true, sliderWidth, 1, true);
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of FieldLines
		
		

		/// <summary>
		/// Defines the Surface menu window, which is opened from the main menu window.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Surface (int a) {
			GUIMoleculeController.Instance.showSurfaceMenu = SetTitleExit("Surface");
			
			GUILayout.BeginHorizontal ();
			GUIMoleculeController.Instance.generateThreshold = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.generateThreshold, 0.002f, 4f, "T:" + Mathf.Round (GUIMoleculeController.Instance.generateThreshold * 10f) / 10f,
									"Determines ramp value for surface generation", true, (int) (0.5 * Rectangles.surfaceMenuWidth), 1);
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			GUI.enabled = UIData.Instance.hasMoleculeDisplay; // trying to generate a surface without a molecule would generate an error
			if (GUILayout.Button (new GUIContent ("Generate", "Generate a new surface mesh"))) {

				if(UIData.Instance.toggleBfac || showSurface){
					UIData.Instance.toggleBfac = false;
					GUIMoleculeController.Instance.pdbGen = false;
					showSurface = false;
					GameObject[] Existpdbden = GameObject.FindGameObjectsWithTag("pdb2den");
					GameObject[] ExistSurf = GameObject.FindGameObjectsWithTag ("SurfaceManager");
					foreach(GameObject s in ExistSurf)
						GameObject.Destroy(s);
					foreach(GameObject s in Existpdbden)
						GameObject.Destroy(s);					
				}

				if (!GUIMoleculeController.Instance.pdbGen) {	
					MoleculeModel.surfaceFileExists = true;
					
					GameObject pdb2den = new GameObject("pdb2den OBJ");
					pdb2den.tag = "pdb2den";
					pdb2den.AddComponent<PDBtoDEN>();
					PDBtoDEN generatedensity = pdb2den.GetComponent<PDBtoDEN>();
					
					generatedensity.TranPDBtoDEN ();
					GUIMoleculeController.Instance.pdbGen = true;
					GUIMoleculeController.Instance.buildSurface = true;
				}

				if(!showSurface) {
					PDBtoDEN.ProSurface (GUIMoleculeController.Instance.generateThreshold);
					showSurface = true;
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal ();
			GUI.enabled = UIData.Instance.hasMoleculeDisplay; // trying to generate a surface without a molecule would generate an error
			if (GUILayout.Button (new GUIContent ("BFactor", "Generate a new surface mesh using b-factors"))) {
				if(showSurface){
					showSurface = false;
					GUIMoleculeController.Instance.pdbGen = false;
					GameObject[] ExistSurf = GameObject.FindGameObjectsWithTag ("SurfaceManager");
					GameObject[] Existpdbden = GameObject.FindGameObjectsWithTag ("pdb2den");
					foreach(GameObject s in ExistSurf) 
						GameObject.Destroy(s);	
					foreach(GameObject s in Existpdbden)
						GameObject.Destroy(s);	
				}
				
				if (!GUIMoleculeController.Instance.pdbGen) {	
					MoleculeModel.surfaceFileExists = true;
					UIData.Instance.toggleBfac = true;
					
					GameObject pdb2den = new GameObject("pdb2den OBJ");
					pdb2den.tag = "pdb2den";
					pdb2den.AddComponent<PDBtoDEN>();
					PDBtoDEN generatedensity = pdb2den.GetComponent<PDBtoDEN>();
					generatedensity.TranPDBtoDEN ();
					GUIMoleculeController.Instance.pdbGen = true;
					GUIMoleculeController.Instance.buildSurface = true;
				}
				if(!showSurface) {
					PDBtoDEN.ProSurface (GUIMoleculeController.Instance.generateThreshold);
					showSurface = true;
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent("Volumetric", "Display volumetric density"))) {
				GUIMoleculeController.Instance.showVolumetricDensity = !GUIMoleculeController.Instance.showVolumetricDensity;
				
				GameObject volumObj;
				volumObj = GameObject.FindGameObjectWithTag("Volumetric");
				VolumetricDensity volumetric = null;
				
				if (GUIMoleculeController.Instance.showVolumetricDensity) {
					volumObj.AddComponent<VolumetricDensity>(); // adding the script
					volumetric = volumObj.GetComponent<VolumetricDensity>();
					volumetric.Init();
				}
				else {
					volumetric = volumObj.GetComponent<VolumetricDensity>();
					volumetric.Clear();
				}
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal();
			GUI.enabled = (GUIMoleculeController.Instance.buildSurface || MoleculeModel.surfaceFileExists);			
			if (GUILayout.Button (new GUIContent ("Toggle surface", "Toggles surface from visible to hidden and vice versa"))) {
				if (showSurface) {
					showSurface = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
					GUIMoleculeController.Instance.buildSurfaceDone = false;
					GUIMoleculeController.Instance.surfaceTextureDone = false;
					GameObject[] SurfaceManager = GameObject.FindGameObjectsWithTag ("SurfaceManager");
					foreach (GameObject Surface in SurfaceManager) {
//						Surface.SetActiveRecursively (false);
//						Surface.SetActive (false);
						Surface.GetComponent<Renderer>().enabled = false;
					}
				} else {
					showSurface = true;
					GameObject[] SurfaceManager = GameObject.FindGameObjectsWithTag ("SurfaceManager");
					foreach (GameObject Surface in SurfaceManager) {
//						Surface.SetActiveRecursively (false);
						Surface.GetComponent<Renderer>().enabled = true;
					}
				}
			}	
			GUILayout.EndHorizontal();


			GUILayout.BeginHorizontal();
			GUI.enabled = true; // otherwise the window might not be draggabl

			MoleculeModel.useHetatmForSurface = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(MoleculeModel.useHetatmForSurface,
			                                                              new GUIContent("HetAtoms", "Use Hetero atoms for surface calculation"));
			MoleculeModel.useSugarForSurface = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(MoleculeModel.useSugarForSurface,
			                                                                                  new GUIContent("Sugars", "Use sugar for surface calculation"));
			GUILayout.EndHorizontal();

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.enabled = true; // otherwise the window might not be draggable
			GUI.DragWindow();
		} // End of Surface
		

		/// <summary>
		/// Defines the Surface Parameters menu window. The latter is automatically opened when a surface
		/// is generated, in the Surface window.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void SurfaceParams (int a) {
			SetTitle("Parameters");

			
			if (GUILayout.Button (new GUIContent ("Color", "Choose the color of the surface")))
				GUIMoleculeController.Instance.CreateColorPicker(GUIMoleculeController.Instance.SurfaceGrayColor,"Surface Color", null);
			
			if (GUILayout.Button (new GUIContent ("Inside color", "Choose the color of the inside of the surface")))
				GUIMoleculeController.Instance.CreateColorPicker(GUIMoleculeController.Instance.SurfaceInsideColor, "Surface inside color", null);
	
			if(GUIMoleculeController.Instance.toggle_NA_HIDE)
				GUI.enabled = false;
			if (GUILayout.Button (new GUIContent ("Use atom color", "Enable/Disable the colors of the atoms on the surface (\"Hide\" must be off)"))) {
				GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
				SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
				GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
//				Debug.Log(surfaceObjs.Length);
				foreach(GameObject surfaceObj in surfaceObjs) {
					if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
						surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
						surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
					}
					else{
//						surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
						surfaceManager.InitTree();
						surfaceManager.ColorVertices();
						// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
						// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
						break; // So you must break...
					}
				}
			}
			GUI.enabled = true;

			if (GUILayout.Button (new GUIContent ("Use chain color", "Enable/Disable the colors of the chain on the surface (\"Hide\" must be off)"))) {
				UIData.Instance.surfColChain = !UIData.Instance.surfColChain;

				if(UIData.Instance.surfColChain){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					//				Debug.Log(surfaceObjs.Length);
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColChain = false;
				}
			}

			if (GUILayout.Button (new GUIContent ("Hydrophobic scale", "Open Hydrophobic scales Menu"))) {
				UI.GUIMoleculeController.Instance.showHydroMenu = !UI.GUIMoleculeController.Instance.showHydroMenu;
			}

			if (GUILayout.Button (new GUIContent ("Use properties color", "Show amino acids properties (red/acid ; blue/basic ; green/polar ; white/apolar) (\"Hide\" must be off)"))) {
				UIData.Instance.surfColPChim = !UIData.Instance.surfColPChim;
				
				if(UIData.Instance.surfColPChim){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					//				Debug.Log(surfaceObjs.Length);
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColPChim = false;
				}
			}

			if (GUILayout.Button (new GUIContent ("Use BFactor color", "Show B-Factor values (\"Hide\" must be off)"))) {
				UIData.Instance.surfColBF = !UIData.Instance.surfColBF;
				Debug.Log ("ColBF " + UIData.Instance.surfColBF);
				
				if(UIData.Instance.surfColBF){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					//				Debug.Log(surfaceObjs.Length);
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColBF = false;
				}


			}

				
			if (GUILayout.Button (new GUIContent ("Texture", "Choose the texture of the surface"))) 
				GUIMoleculeController.Instance.showSurfaceTexture = !GUIMoleculeController.Instance.showSurfaceTexture;
			
			if (GUILayout.Button (new GUIContent ("Static cut", "Activate a cut plane on the surface"))) {
				if (GUIMoleculeController.Instance.surfaceStaticCut) {
					GUIMoleculeController.Instance.surfaceStaticCut = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
				} else {
					GUIMoleculeController.Instance.surfaceMobileCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
					GUIMoleculeController.Instance.surfaceStaticCut = true;
					GUIMoleculeController.Instance.showSurfaceCut = true;
				}
			}
			
			if (GUILayout.Button (new GUIContent ("Mobile cut", "Activate a mobile cut plane on the surface"))) {
				if (GUIMoleculeController.Instance.surfaceMobileCut) {
					GUIMoleculeController.Instance.surfaceMobileCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
				} else {
					GUIMoleculeController.Instance.surfaceStaticCut = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
					GUIMoleculeController.Instance.surfaceMobileCut = true;
					GUIMoleculeController.Instance.showSurfaceMobileCut = true;
				}
			}
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Brightness: " + SurfaceManager.brightness.ToString("0.00"));
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			int sliderWidth = (int) (Rectangles.surfaceMenuWidth * 0.9f);
			SurfaceManager.brightness = GUIMoleculeController.Instance.LabelSlider(SurfaceManager.brightness, 0.33f, 2.0f, "",
										"Adjust the brightness of the surface", true, sliderWidth, 0, false);
			if(GUI.changed)
				SurfaceManager.resetBrightness = true;			
			GUILayout.EndHorizontal();
			
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Color weight: " + SurfaceManager.colorWeight.ToString("0.00"));
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			SurfaceManager.colorWeight = GUIMoleculeController.Instance.LabelSlider(SurfaceManager.colorWeight, 0f, 1f, "",
										"Adjust the weight of the vertex colors of the surface", true, sliderWidth, 0, false);
			if(GUI.changed)
				SurfaceManager.resetColorWeight = true;			
			GUILayout.EndHorizontal();
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of Surface Params

		public void HydroMenu (int a) {
			SetTitle("Hydrophobic scales");

			if (GUILayout.Button (new GUIContent ("Kyte & Doolittle", "Surface coloring by using Kyte and Doolittle hydrophobic scale"))) {
				UIData.Instance.surfColHydroKD = !UIData.Instance.surfColHydroKD;
				
				if(UIData.Instance.surfColHydroKD){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColHydroKD = false;
				}
			}

			if (GUILayout.Button (new GUIContent ("Engleman & al.", "Surface coloring by using Engleman & al hydrophobic scale"))) {
				UIData.Instance.surfColHydroEng = !UIData.Instance.surfColHydroEng;
				
				if(UIData.Instance.surfColHydroEng){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColHydroEng = false;
				}
			}

			if (GUILayout.Button (new GUIContent ("Eisenberg", "Surface coloring by using Eisenberg scale"))) {
				UIData.Instance.surfColHydroEis = !UIData.Instance.surfColHydroEis;
				
				if(UIData.Instance.surfColHydroEis){
					GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
					SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
					GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
					foreach(GameObject surfaceObj in surfaceObjs) {
						if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
							surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
							surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
						}
						else{
							//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
							surfaceManager.InitTree();
							surfaceManager.ColorVertices();
							// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
							// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
							break; // So you must break...
						}
					}
					UIData.Instance.surfColHydroEis = false;
				}
			}

				if (GUILayout.Button (new GUIContent ("White Octanol", "Surface coloring by using White Octanol scale"))) {
					UIData.Instance.surfColHydroWO = !UIData.Instance.surfColHydroWO;
					
					if(UIData.Instance.surfColHydroWO){
						GameObject surfaceManagerObj = GameObject.FindGameObjectWithTag("NewSurfaceManager");
						SurfaceManager surfaceManager = surfaceManagerObj.GetComponent<SurfaceManager>();
						GameObject[] surfaceObjs = GameObject.FindGameObjectsWithTag("SurfaceManager");
						foreach(GameObject surfaceObj in surfaceObjs) {
							if(surfaceObj.GetComponent<Renderer>().material.shader == Shader.Find("Vertex Colored")){
								surfaceObj.GetComponent<Renderer>().material = new Material(Shader.Find("Mat Cap Cut"));
								surfaceObj.GetComponent<Renderer>().material.SetTexture("_MatCap", (Texture)Resources.Load ("lit_spheres/divers/daphz1"));
							}
							else{
								//surfaceObj.renderer.material = new Material(Shader.Find("Vertex Colored"));
								surfaceManager.InitTree();
								surfaceManager.ColorVertices();
								// Init and ColorVertice already change the shader of all the SurfaceOBJ. So if you continue the foreach
								// you'll find "VertexColored" for the next shaders and it will replace them by MatCapCut
								break; // So you must break...
							}
						}
						UIData.Instance.surfColHydroWO = false;
					}
			}

		} // End of HydroMenu
		
		

		/// <summary>
		/// Defines the Background selection window.
		/// It is automatically opened when the background is toggled, from the Display menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Background (int a) {
			GUIMoleculeController.Instance.showBackgroundType = SetTitleExit("Background");
//			GameObject LocCamera = GameObject.Find ("Camera");

			GUILayout.BeginHorizontal ();

//			if (GUILayout.Button (new GUIContent ("1", "Lerpz background")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/skyBoxLerpzMaterial");		
//
//			if (GUILayout.Button (new GUIContent ("2", "HotDesert background")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/skyBoxHotDesert");
//
//			if (GUILayout.Button (new GUIContent ("3", "Molecule background")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/skyBoxmolecularMaterial");
//			
//			if (GUILayout.Button (new GUIContent ("4", "Snow background")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/skyBoxSnow");
						
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
//			if (GUILayout.Button (new GUIContent ("5", "DawnDusk Skybox")))
//							LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/DawnDusk Skybox");				
//
//			if (GUILayout.Button (new GUIContent ("6", "Eerie Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Eerie Skybox");				
//
//			if (GUILayout.Button (new GUIContent ("7", "MoonShine Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/MoonShine Skybox");				
//
//			if (GUILayout.Button (new GUIContent ("8", "Overcast1 Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Overcast1 Skybox");				

			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			
//			if (GUILayout.Button (new GUIContent ("9", "Overcast2 Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Overcast2 Skybox");
//
//			if (GUILayout.Button (new GUIContent ("10", "StarryNight Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/StarryNight Skybox");				
//
//			if (GUILayout.Button (new GUIContent ("11", "Sunny1 Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Sunny1 Skybox");				

			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			
//			if (GUILayout.Button (new GUIContent ("12", "Sunny2 Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Sunny2 Skybox");				
//
//			if (GUILayout.Button (new GUIContent ("13", "Sunny3 Skybox")))
//				LocCamera.GetComponent<Skybox> ().material = (Material)Resources.Load ("skybox/Sunny3 Skybox");				

			GUILayout.EndHorizontal ();

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of Background
		
		

		/// <summary>
		/// Defines the surface cut window, which is opened from the Surface parameters menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void SurfaceCut (int a) {
			GUIMoleculeController.Instance.showSurfaceCut = SetTitleExit("Cut Parameters");
			GUIMoleculeController.Instance.surfaceStaticCut = GUIMoleculeController.Instance.showSurfaceCut; // To disable the cut along with the window.
			int sliderWidth = (int) (Rectangles.surfaceCutWidth * 0.80f);
			
			GUIMoleculeController.Instance.depthCut = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.depthCut, GUIMoleculeController.Instance.depthCutMin, GUIMoleculeController.Instance.depthCutMax, "Depth " + GUIMoleculeController.Instance.depthCut.ToString("0.00"), 
									"Determines cut plane depth position", true, sliderWidth, 1, true); 
			GUIMoleculeController.Instance.cutX = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.cutX, -1f, 1f, " X: " + GUIMoleculeController.Instance.cutX.ToString("0.00"),
									"Determines cut plane X position", true, sliderWidth, 1, true); 
			GUIMoleculeController.Instance.cutY = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.cutY, -1f, 1f, " Y: " + GUIMoleculeController.Instance.cutY.ToString("0.00"),
									"Determines cut plane Y position", true, sliderWidth, 1, true); 
			GUIMoleculeController.Instance.cutZ = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.cutZ, -1f, 1f, " Z: " + GUIMoleculeController.Instance.cutZ.ToString("0.00"),
									"Determines cut plane Z position", true, sliderWidth, 1, true);
			
			GUI.enabled = true;
			GUI.DragWindow();
		}
		
		
		
		/// <summary>
		/// Defines the mobile surface cut window, which is opened from the Surface parameters menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void SurfaceMobileCut (int a) {
			int sliderWidth = (int) (Rectangles.surfaceCutWidth * 0.45f);
			SetTitle("Surface Mobile Cut");
			GUIMoleculeController.Instance.depthCut = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.depthCut, -40f, 40f, "Cutting depth " + GUIMoleculeController.Instance.depthCut,
									"Determines mobile cut plane depth position", true, sliderWidth, 1); 
			GUIMoleculeController.Instance.adjustFieldLineCut = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.adjustFieldLineCut, -100f, 100f, " FL cut :" + GUIMoleculeController.Instance.adjustFieldLineCut,
									"Determines field line cut position", true, sliderWidth, 1); 
			GUI.DragWindow();
		}
		
		
		
		/// <summary>
		/// Defines the Effect selection window for SSAO, DOF, etc. Opened from the Display menu.
		/// </summary>
		/// <param name='a'>
		/// A.
		/// </param>
		public void Effects (int a) {
			GUIMoleculeController.Instance.showEffectType = SetTitleExit("Visual Effects");
			
			GUILayout.BeginHorizontal ();
			toggle_VE_SSAO = GUILayout.Toggle (toggle_VE_SSAO, new GUIContent ("SSAO", "Toggle screen space ambient occlusion effect"));
			if (!toggle_VE_SSAO) { 
				if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SSAOEffect> ().enabled) 
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SSAOEffect> ().enabled = false;
			}
			else { 
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SSAOEffect>().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SSAOEffect>().m_Radius = 4f;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SSAOEffect>().m_OcclusionIntensity = 1f;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for BLUR ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE_BLUR = GUILayout.Toggle (toggle_VE_BLUR, new GUIContent ("BLUR", "Toggle motion blur effect"));
			if (!toggle_VE_BLUR && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur> ().enabled = false;
			else if (toggle_VE_BLUR && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur> ().enabled) { 
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur> ().shader = Shader.Find ("Hidden/MotionBlur");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MotionBlur> ().enabled = true;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for NOISE :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_NOISE = GUILayout.Toggle (toggle_VE2_NOISE, new GUIContent ("NOISE", "Toggle noise effect"));
			if (!toggle_VE2_NOISE && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().enabled = false;
			else if (toggle_VE2_NOISE && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <NoiseEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().shaderRGB = Shader.Find ("Hidden/Noise Shader RGB");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().shaderYUV = Shader.Find ("Hidden/Noise Shader YUV");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().grainTexture = Resources.Load ("NoiseEffectGrain") as Texture2D;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NoiseEffect> ().scratchTexture = Resources.Load ("NoiseEffectScratch") as Texture2D;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for BLUR2 :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_BLUR2 = GUILayout.Toggle (toggle_VE2_BLUR2, new GUIContent ("BLUR2", "Toggle overall blur effect"));
			if (!toggle_VE2_BLUR2 && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect> ().enabled = false;
			else if (toggle_VE2_BLUR2 && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect> ().enabled) { 
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect> ().blurShader = Shader.Find ("Hidden/BlurEffectConeTap");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BlurEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal();
			// Make a toggle for DOF :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE_DOF = GUILayout.Toggle (toggle_VE_DOF, new GUIContent ("DOF", "Toggle depth of field effect."));
			if (!toggle_VE_DOF && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().enabled) {
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().enabled = false;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectAtomFocus>().enabled = false;
			} else if (toggle_VE_DOF && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <DepthOfFieldScatter>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().dofHdrShader = Shader.Find("Hidden/Dof/DepthOfFieldHdr");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().focalSize = 0;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().enabled = true ;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().aperture = 25f;
				Debug.Log(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DepthOfFieldScatter>().aperture);
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectAtomFocus>().enabled = true;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for Crease :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE_CREASE = GUILayout.Toggle (toggle_VE_CREASE, new GUIContent ("CREASE", "Toggle crease effect"));
			if (!toggle_VE_CREASE && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease>().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease> ().enabled = false;
			else if (toggle_VE_CREASE && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <Crease>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease> ().creaseApplyShader = Shader.Find("Hidden/CreaseApply");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease> ().intensity = 20;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Crease> ().enabled = true ;
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			// Make a toggle for EDGE :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_EDGE = GUILayout.Toggle (toggle_VE2_EDGE, new GUIContent ("EDGE", "Toggle edge detection effect"));
			if (!toggle_VE2_EDGE && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EdgeDetectEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EdgeDetectEffect> ().enabled = false;
			else if (toggle_VE2_EDGE && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EdgeDetectEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <EdgeDetectEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EdgeDetectEffect> ().shader = Shader.Find ("Hidden/Edge Detect X");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EdgeDetectEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for VORTX :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_VORTX = GUILayout.Toggle (toggle_VE2_VORTX, new GUIContent ("VORTX", "Toggle vortex deformation effect"));
			if (!toggle_VE2_VORTX && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VortexEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VortexEffect> ().enabled = false;
			else if (toggle_VE2_VORTX && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VortexEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <VortexEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VortexEffect> ().shader = Shader.Find ("Hidden/Twist Effect");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VortexEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();

			// Make a combined toggle+button for GRAYS ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			GUILayout.BeginHorizontal ();
			toggle_VE2_GRAYS = GUILayout.Toggle (toggle_VE2_GRAYS, new GUIContent ("GRAYS", "Toggle grayscale color effect"));
			if (!toggle_VE2_GRAYS && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().enabled = false;
			else if (toggle_VE2_GRAYS && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <GrayscaleEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().shader = Shader.Find ("Hidden/Grayscale Effect");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().textureRamp = Resources.Load (ve2_grays_ramps [ve2_grays_rampc]) as Texture2D;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Ramp " + ve2_grays_rampc, "Choose among several grayscale ramps"))) {
				ve2_grays_rampc += 1;
				if (ve2_grays_rampc > ve2_grays_rampn)
					ve2_grays_rampc = 0;

				if (toggle_VE2_GRAYS)
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GrayscaleEffect> ().textureRamp = Resources.Load (ve2_grays_ramps [ve2_grays_rampc]) as Texture2D;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for TWIRL :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_TWIRL = GUILayout.Toggle (toggle_VE2_TWIRL, new GUIContent ("TWIRL", "Toggle twirl deformation effect"));
			if (!toggle_VE2_TWIRL && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwirlEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwirlEffect> ().enabled = false;
			else if (toggle_VE2_TWIRL && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwirlEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <TwirlEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwirlEffect> ().shader = Shader.Find ("Hidden/Twirt Effect Shader");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TwirlEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();
			
			
/*
			// Make a combined toggle+button for CCORR ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			GUILayout.BeginHorizontal ();
			toggle_VE2_CCORR = GUILayout.Toggle (toggle_VE2_CCORR, new GUIContent ("CCORR", "Toggle color correction effect"));
			if (!toggle_VE2_CCORR && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().enabled = false;
			else if (toggle_VE2_CCORR && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent ("ColorCorrectionEffect");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().enabled = true;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().shader = Shader.Find ("Hidden/Grayscale Effect");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().textureRamp = Resources.Load (ve2_ccorr_ramps [ve2_ccorr_rampc]) as Texture2D;
			}
			if (GUILayout.Button (new GUIContent ("Ramp " + ve2_ccorr_rampc, "Choose among several color correction ramps"))) {
				ve2_ccorr_rampc += 1;
				if (ve2_ccorr_rampc > ve2_ccorr_rampn)
					ve2_ccorr_rampc = 0;

				if (toggle_VE2_CCORR)
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ColorCorrectionEffect> ().textureRamp = Resources.Load (ve2_ccorr_ramps [ve2_ccorr_rampc]) as Texture2D;
			}
			GUILayout.EndHorizontal ();
*/
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for SEPIA :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_SEPIA = GUILayout.Toggle (toggle_VE2_SEPIA, new GUIContent ("SEPIA", "Toggle sepia tone color effect"));
			if (!toggle_VE2_SEPIA && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SepiaToneEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SepiaToneEffect> ().enabled = false;
			else if (toggle_VE2_SEPIA && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SepiaToneEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <SepiaToneEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SepiaToneEffect> ().shader = Shader.Find ("Hidden/Sepiatone Effect");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SepiaToneEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			// Make a toggle for GLOW :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
			toggle_VE2_GLOW = GUILayout.Toggle (toggle_VE2_GLOW, new GUIContent ("GLOW", "Toggle glow effect"));
			if (!toggle_VE2_GLOW && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().enabled)
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().enabled = false;
			else if (toggle_VE2_GLOW && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().enabled) { 
				GameObject.FindWithTag ("MainCamera").AddComponent <GlowEffect>();
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().compositeShader = Shader.Find ("Hidden/GlowCompose");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().blurShader = Shader.Find ("Hidden/GlowConeTap");
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().downsampleShader = Shader.Find ("Hidden/Glow Downsample");
//				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect>().blurspread = 0.7f;
//				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect>().bluriterations = 3f;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().glowIntensity = 0.3f;
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlowEffect> ().enabled = true;
			}
			GUILayout.EndHorizontal ();

			GUIMoleculeController.Instance.showBackgroundType = false;
			showGrayColor = false;
//			ParamshowFieldLine=false;
			showSurfaceButton = false;
			

//			GUI.Label ( new Rect(120,Screen.height-35,Screen.width-360,20), GUI.tooltip);
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			
			GUI.DragWindow();
		} // End of Effects
		
		
		
		/// <summary>
		/// Defines the cube/line bond window, that lets users define how wide they want their bonds.
		/// Opened from the Atom appearance menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void CubeLineBond (int a){
			SetTitle("Bond Width");

			GUIMoleculeController.Instance.bondWidth = GUIMoleculeController.Instance.LabelSlider(GUIMoleculeController.Instance.bondWidth, 0.00001f, 0.5f, "Width: " + GUIMoleculeController.Instance.bondWidth.ToString("0.00"), 
					"Determines width of bonds for Cubes and Lines", true, (int)(0.90 * Rectangles.cubeLineBondTypeWidth), 1, true);
			GUI.enabled = true;
			GUI.DragWindow();
		}
		
		/// <summary>
		/// Returns true if the mouse cursor is lower than the Window title.
		/// This function assumes that the cursor is contained in the window,
		/// it is not to be called in any other case.
		/// </summary>
		/// <returns>
		/// A boolean.
		/// </returns>
		/// <param name='rect'>
		/// This is the rectangle representing the window.
		/// </param>
		/// <param name='mpos'>
		/// This is the *flipped* mouse position. I don't know why, but Unity returns a Vector3 for this, not a Vector2.
		/// And to make things even more fun, the mouse starts at 0,0 in the bottom left corner and increases to
		/// maxWidth,maxHeight in the top right corner, while rectangles start at 0,0 in the top left corner,
		/// and increase to maxW,maxH in the bottom right corner.
		/// So it's a Vector3, and the y axis must be flipped before mpos is fed to this function.
		/// </param>
		private bool InDeadZone(Rect rect, Vector3 mpos)
		{
			GUIStyle currentStyle = GUI.skin.label;
			GUIContent strContent = new GUIContent("str"); // Creating a GUIContent of type string. Probably not the cleanest way.
			float deadZone = currentStyle.CalcSize(strContent).y; // Getting its height in pixels.
			deadZone *= 3.0f ; // Making it a bit bigger. After all, the title label doesn't start right at the top of the window.
			
			GUIMoleculeController.Instance.cutPlaneIsDraggable = (mpos.y - rect.yMin < deadZone); // if true, we're in the dead zone
			return GUIMoleculeController.Instance.cutPlaneIsDraggable;
		}
		
		/// <summary>
		/// Defines the move cut plane window, that lets the user change the cut plane of the surface by clicking or scrolling.
		/// Opened from the Surface menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void MoveCutPlane (int a) {
			SetTitle("Move cut plane");
			if(GUIMoleculeController.Instance.cutPlaneIsDraggable)
				GUI.DragWindow();
			
			Vector3 mousePos = Input.mousePosition;
			mousePos.y = Screen.height - mousePos.y;
			if (Rectangles.movePlaneRect.Contains(mousePos) && GUIMoleculeController.Instance.showSurfaceCut && GUIMoleculeController.Instance.showSurfaceMenu 
				&& !InDeadZone(Rectangles.movePlaneRect, mousePos) && GUIUtility.hotControl == 0) 
			{
				if (Input.GetMouseButton (0)) {
					GUIMoleculeController.Instance.cutX += Input.GetAxis ("Mouse X") * 1 * 0.02f;
					GUIMoleculeController.Instance.cutY -= Input.GetAxis ("Mouse Y") * 1 * 0.02f;
					GUIMoleculeController.Instance.cutZ -= Input.GetAxis ("Mouse X") * 1 * 0.02f;
				}
				if (GUIMoleculeController.Instance.cutX < -1)
					GUIMoleculeController.Instance.cutX = -1;
				if (GUIMoleculeController.Instance.cutX > 1)
					GUIMoleculeController.Instance.cutX = 1;
				if (GUIMoleculeController.Instance.cutY < -1)
					GUIMoleculeController.Instance.cutY = -1;
				if (GUIMoleculeController.Instance.cutY > 1)
					GUIMoleculeController.Instance.cutY = 1;
				if (GUIMoleculeController.Instance.cutZ < -1)
					GUIMoleculeController.Instance.cutZ = -1;
				if (GUIMoleculeController.Instance.cutZ > 1)
					GUIMoleculeController.Instance.cutZ = 1;
				GUIMoleculeController.Instance.depthCut -= Input.GetAxis ("Mouse ScrollWheel");
//				cutZ +=Input.GetAxis("Mouse X") * 1 * 0.02f;
//				cutZ -=Input.GetAxis("Mouse Y") * 1 * 0.02f;
			}
		}
		
		
		/// <summary>
		/// Just triggers the metaphor menu. Part of the Hyperball Style window.
		/// </summary>
		private void MetaphorControl () {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Metaphor", "Change HyperBalls parameters to values for standard representations")))
				GUIMoleculeController.Instance.showMetaphorType = !GUIMoleculeController.Instance.showMetaphorType;
			GUILayout.EndHorizontal ();
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}
		
		
		/// <summary>
		/// This function defines the GUI components that let users choose the colors used in interactive mode.
		/// When in interactive mode, toggling 'Gray' will turn the molecule gray, and the higher the velocity
		/// of an atom/bond, the darker it will be.
		/// </summary>
		private void PhysicalChoice () {
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Velocity Colors:");
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			
			UIData.Instance.toggleGray = GUILayout.Toggle (UIData.Instance.toggleGray, "Gray");
			if (UIData.Instance.toggleGray)
				UIData.Instance.toggleColor = false;
			else
				UIData.Instance.toggleColor = true;
			
			UIData.Instance.toggleColor = GUILayout.Toggle (UIData.Instance.toggleColor, "Normal");
			if (UIData.Instance.toggleColor)
				UIData.Instance.toggleGray = false;
			else
				UIData.Instance.toggleGray = true;
			
			GUILayout.EndHorizontal ();
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}
		
		
		/// <summary>
		/// Defines the Hyperball Style window. opened from the Atom Appearance menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void HyperballStyle (int a) {
			int sliderWidth = (int)(0.40 * Rectangles.hyperballWidth);
			SetTitle("Hyperball Style");
			GUIMoleculeController.Instance.shrink = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.shrink, 0.00001f, 0.99f, "Shrink " + GUIMoleculeController.Instance.shrink.ToString("0.00"),
				"Determines shrink factor parameter for HyperBalls", true, sliderWidth, 1);
			
//			toggle_HB_RANIM = GUILayout.Toggle (toggle_HB_RANIM, new GUIContent ("HB_RANIM", "Animate radius parameter"));
			GUIMoleculeController.Instance.linkScale = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.linkScale, 0.00001f, 1.0f, "Scale " + GUIMoleculeController.Instance.linkScale.ToString("0.00"),
				"Determines scale parameter", true, sliderWidth, 1);
			
			GUIMoleculeController.Instance.depthfactor = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.depthfactor, -3.0f, 3.0f, "DFactor " + GUIMoleculeController.Instance.depthfactor.ToString("0.00"),
				"Determines depth factor for network visualization", MoleculeModel.networkLoaded, sliderWidth, 1);
			GUI.enabled = true;
			
			MetaphorControl ();
			
			if(UIData.Instance.atomtype != UIData.AtomType.hyperball){
				GUI.enabled = false;
				GUIMoleculeController.Instance.toggle_NA_INTERACTIVE = false;
			}
			GUIMoleculeController.Instance.toggle_NA_INTERACTIVE = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_NA_INTERACTIVE, new GUIContent ("Interactive mode", "Toggle interactive mode, the physics engine will be used"));
			GUI.enabled = true;
			GUIMoleculeController.Instance.drag = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.drag, 0.00001f, 5f, "Drag " + GUIMoleculeController.Instance.drag.ToString("0.00"), "Determines PhysX engine drag value", 
									UIData.Instance.interactive, sliderWidth, 1);
			GUIMoleculeController.Instance.spring = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.spring, 0.00001f, 20, "Spring " + GUIMoleculeController.Instance.spring.ToString("0.00"), "Determines PhysX engine spring constant",
									UIData.Instance.interactive, sliderWidth, 1);
			PhysicalChoice();

			// Luiz FIXME: gotta find a way to synchronize this before enabling it
			GUIMoleculeController.Instance.toggle_NA_INTERACTIVE=false;

			GUI.enabled = true;	
			
			GUILayout.BeginHorizontal();
			toggle_NA_MEASURE = GUILayout.Toggle (toggle_NA_MEASURE, new GUIContent ("Measure dist.", "Toggle mouse clicking to measure the distance between two atoms"));
			GUILayout.EndHorizontal();

			// Luiz FIXME gotta find a way to synchronize selection
			toggle_NA_MEASURE = false;
			
			GUILayout.BeginHorizontal();
			GUIMoleculeController.Instance.toggle_DISTANCE_CUEING = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_DISTANCE_CUEING, new GUIContent ("Dist. cueing", "Toggle distance cueing, which darkens distant objects"));
			GUILayout.EndHorizontal();
			
			if(GUIMoleculeController.Instance.toggle_DISTANCE_CUEING) {
				if(!GUIMoleculeController.Instance.distanceCueingEnabled)
					DisplayMolecule.ToggleDistanceCueing(true);
					GUIMoleculeController.Instance.distanceCueingEnabled = true;
				}
			else
				if(GUIMoleculeController.Instance.distanceCueingEnabled) {
					DisplayMolecule.ToggleDistanceCueing(false);
					GUIMoleculeController.Instance.distanceCueingEnabled = false;
				}


			//Alex :
			//TODO : LOD mode buggy with other representation than Hyperball

			GUI.enabled = true;
			if (!toggle_NA_MEASURE && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MeasureDistance> ())
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MeasureDistance> ().enabled = false;
			else if (toggle_NA_MEASURE && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MeasureDistance> ())
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MeasureDistance> ().enabled = true;
//			GUILayout.EndHorizontal();
			


			//////////////////////modify///////////////////////
			
			if (GUIMoleculeController.Instance.toggle_NA_INTERACTIVE && UIData.Instance.atomtype == UIData.AtomType.hyperball) {
				UIData.Instance.interactive = true;
				UIData.Instance.resetInteractive = true;
			} else {
				UIData.Instance.toggleGray = false;
				UIData.Instance.toggleColor = true;
				UIData.Instance.interactive = false;
				//UIData.Instance.resetInteractive = true;
				if (!MoleculeModel.fieldLineFileExists)
					showGrayColor = false;
				if (!MoleculeModel.surfaceFileExists)
					showSurfaceButton = false;
			}
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			GUI.DragWindow();
		} // End of HyperballStyle

		[System.Serializable]
		public class hey {
			public System.Collections.Generic.List<hoy> hoys;
		}

		[System.Serializable]
		public class hoy {
			public string a;
		}
		
		/// <summary>
		/// Defines the main menu of the GUI.
		/// Controls the main menus.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void MainFun (int a) {
			if(GUIMoleculeController.Instance.toggle_NA_HIDE)
				GUIMoleculeController.Instance.Molecule3DComp.HideAtoms();
			else
				GUIMoleculeController.Instance.Molecule3DComp.ShowAtoms();
			
//			GUILayout.BeginVertical();
			
			GUILayout.BeginHorizontal();
//			if (GUI.Button (new Rect(300,5,40,10),new GUIContent ("Open", "Open the File Open dialogue"))) {
			if (GUILayout.Button (new GUIContent ("File", "Open the File Open dialogue"))) {
				if (GUIMoleculeController.Instance.showOpenMenu)
					GUIMoleculeController.Instance.showOpenMenu = false;
				else {
					GUIMoleculeController.Instance.showOpenMenu = true;
					GUIMoleculeController.Instance.showAtomMenu = false;
					GUIMoleculeController.Instance.showSurfaceMenu = false;
					GUIMoleculeController.Instance.showBfactorMenu = false;
					GUIMoleculeController.Instance.showElectrostaticsMenu = false;
					GUIMoleculeController.Instance.fieldLineColorGradient = false;
					GUIMoleculeController.Instance.showManipulatorMenu = false;
					GUIMoleculeController.Instance.showSetAtomScales = false;
					GUIMoleculeController.Instance.showPanelsMenu = false;
					GUIDisplay.Instance.m_texture = false;
					GUIMoleculeController.Instance.m_colorPicker = null;
					showSurfaceButton = false;
					GUIMoleculeController.Instance.showBackgroundType = false;
					GUIMoleculeController.Instance.showSurfaceCut = false;
					GUIMoleculeController.Instance.showSurfaceMobileCut = false;
					GUIMoleculeController.Instance.showSurfaceTexture = false;
					GUIMoleculeController.Instance.showAtomsExtendedMenu = false;
					GUIMoleculeController.Instance.showResiduesMenu = false;
					GUIMoleculeController.Instance.showChainsMenu = false;
					toggle_NA_SWITCH = false;
					GUIMoleculeController.Instance.showSugarChainMenu=false; //T TEST
					GUIMoleculeController.Instance.showVRPNMenu = false;
					GUIMoleculeController.Instance.showMDDriverMenu = false;
				}

			}
			
			if(!UIData.Instance.hasMoleculeDisplay)
				GUI.enabled = false;
			if (GUILayout.Button (new GUIContent ("Atoms", "Open the Atom appearance dialogue"))) {
				if (GUIMoleculeController.Instance.showAtomMenu) { // already open, we close it
					GUIMoleculeController.Instance.showAtomMenu = false;
					GUIDisplay.Instance.m_texture = false ; // this is pointless when the atom menu is closed
					GUIMoleculeController.Instance.showSetAtomScales = false;
				} else {
					GUIMoleculeController.Instance.showAtomMenu = true;
					GUIMoleculeController.Instance.showOpenMenu = false;
				}
			}
			
			if(GUILayout.Button(new GUIContent("Sec. Structures", "Open the secondary structures dialogue")))
				GUIMoleculeController.Instance.showSecStructMenu = !GUIMoleculeController.Instance.showSecStructMenu;
			
			if (GUILayout.Button (new GUIContent ("Surface", "Open the Surface rendering dialogue"))) {
				if (GUIMoleculeController.Instance.showSurfaceMenu) {
					GUIMoleculeController.Instance.showSurfaceMenu = false;
//					GUIMoleculeController.Instance.showSurfaceCut=false;
//					GUIMoleculeController.Instance.showSurfaceMobileCut=false;
					GUIMoleculeController.Instance.showSurfaceTexture = false;
				} else {
					GUIMoleculeController.Instance.showSurfaceMenu = true;
					GUIMoleculeController.Instance.showBfactorMenu = false;
					GUIMoleculeController.Instance.showOpenMenu = false;

				}
				if (!UIData.Instance.toggleSurf) {
					UIData.Instance.toggleBfac = false;
					UIData.Instance.toggleSurf = true;
					GUIMoleculeController.Instance.pdbGen = false;
				}
			}
			//No bfactor option in this version
/*			if (GUI.Button (new Rect (240 + 23, 2, 80, 20), new GUIContent ("Bfactor", "Open the Bfactor settings dialogue"))) 
			{
				if (GUIMoleculeController.Instance.showBfactorMenu) {
					GUIMoleculeController.Instance.showBfactorMenu = false;
			 		GUIMoleculeController.Instance.showSurfaceCut = false;
			 		GUIMoleculeController.Instance.showSurfaceMobileCut = false;
			 		m_colorpick_fieldline = null;	
			 		GUIMoleculeController.Instance.showSurfaceTexture = false;
			 		;
			 		m_colorpick_Surface = null;
				} else {
			 		GUIMoleculeController.Instance.showBfactorMenu = true;
			 		GUIMoleculeController.Instance.showSurfaceMenu = false;	
			 		GUIMoleculeController.Instance.showOpenMenu = false;

				}
			 	if (!UIData.Instance.toggleBfac) {
			 		UIData.Instance.toggleBfac = true;
			 		UIData.Instance.toggleSurf = false;
			 		GUIMoleculeController.Instance.pdbGen = false;
			 	}
			}
*/
			if(!MoleculeModel.dxFileExists)
				GUI.enabled = false;
			if (GUILayout.Button (new GUIContent ("Electrostat.", "Open the electrostatics field lines dialogue"))) {
				if (GUIMoleculeController.Instance.showElectrostaticsMenu) {
					GUIMoleculeController.Instance.showElectrostaticsMenu = false;
					showGrayColor = false;
					GUIMoleculeController.Instance.m_colorPicker = null ;
				} else {
					GUIMoleculeController.Instance.showElectrostaticsMenu = true;
					GUIMoleculeController.Instance.showOpenMenu = false;

				}
			}
			GUI.enabled = true;
			
			if(!UIData.Instance.hasMoleculeDisplay)
				GUI.enabled = false;

			if (GUILayout.Button (new GUIContent ("Display", "Open display configuration menu")))
				GUIMoleculeController.Instance.showManipulatorMenu = !GUIMoleculeController.Instance.showManipulatorMenu;

			// Luiz: Could've changed the buttons' order, so the conditional below would be executed only once,
			// but I don't know how that would have affected to windows' positions on the screen.
			GUI.enabled = true;			
			if (GUILayout.Button(new GUIContent("Advanced", "Opens the advanced options menu")))
				GUIMoleculeController.Instance.showAdvMenu = !GUIMoleculeController.Instance.showAdvMenu ;
			if(!UIData.Instance.hasMoleculeDisplay)
				GUI.enabled = false;

			if (GUILayout.Button (new GUIContent ("Guided Nav.", "Opens the guided navigation menu")))
				GUIMoleculeController.Instance.showGuidedMenu = !GUIMoleculeController.Instance.showGuidedMenu ;
			
			if (GUILayout.Button(new GUIContent("Sugar", "Opens the Sugar visualisation menu"))) //T TEST
				GUIMoleculeController.Instance.showSugarChainMenu = !GUIMoleculeController.Instance.showSugarChainMenu ;
			
			if (GUILayout.Button (new GUIContent("VRPN", "Configure and run a VRPN client"))) {
				GUIMoleculeController.Instance.showVRPNMenu = !GUIMoleculeController.Instance.showVRPNMenu;
				if(VRPNManager.vrpnManager == null) {
					Debug.Log ("Creating VRPN Manager");
					VRPNManager.vrpnManager = (GameObject) GameObject.Instantiate(Resources.Load("VRPN/VRPNManager", typeof(GameObject)), Vector3.zero, Quaternion.identity);
					VRPNManager.vrpnManagerScript = VRPNManager.vrpnManager.GetComponent<VRPNManager>();
				}
			}
			
			if (GUILayout.Button (new GUIContent("MDDriver", "Configure and run a molecular dynamics simulation"))) {
				GUIMoleculeController.Instance.showMDDriverMenu = !GUIMoleculeController.Instance.showMDDriverMenu;
			}
			
			if (GUILayout.Button (new GUIContent("Reset", "Resets the molecule to its original position"))) {
				maxCamera fixeCam;
				fixeCam = GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ();
				fixeCam.ToCenter();
				if(UIData.Instance.atomtype == UIData.AtomType.hyperball){
					GameObject hbManagerObj = GameObject.FindGameObjectWithTag("HBallManager");
					HBallManager hbManager = hbManagerObj.GetComponent<HBallManager>();
					hbManager.ResetPositions();
				}
			}
			
			
			GUILayout.EndHorizontal();
			GUI.enabled = true;
			
			//GUILayout.EndVertical();
			
			// generate the cam target.
			if (!GUIMoleculeController.Instance.toggle_NA_MAXCAM && GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().enabled) {
				GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().enabled = false;
				GUIMoleculeController.Instance.scenecontroller.transform.rotation = NA_SCCROT;
				GUIMoleculeController.Instance.scenecontroller.transform.position = NA_SCCPOS;
			} else if (GUIMoleculeController.Instance.toggle_NA_MAXCAM && !GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().enabled)
				GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().enabled = true;				
				
			if (!GUIMoleculeController.Instance.toggle_NA_AUTOMOVE && GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().automove) {
				GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().automove = false;	
			 	GUIMoleculeController.Instance.Molecule3DComp.toggleFPSLog ();
			} else if (GUIMoleculeController.Instance.toggle_NA_AUTOMOVE && !GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().automove) {
			 	GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ().automove = true;
			 	GUIMoleculeController.Instance.Molecule3DComp.toggleFPSLog ();
			}
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;

		} // End of MainFun
		
		/// <summary>
		/// Defines the menu for handling secondary structures (with ribbons)
		/// </summary>
		/// <param name='a'>
		/// Window identifier
		/// </param>
		public void SecStructMenu(int a) {
			GUIMoleculeController.Instance.showSecStructMenu = SetTitleExit("Secondary Structures");
			bool ssToggled = GUIMoleculeController.Instance.toggle_SUGAR_ONLY;

			GUILayout.BeginHorizontal();
			GUILayout.Box("Secondary structures");
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUIMoleculeController.Instance.toggle_SUGAR_ONLY = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(GUIMoleculeController.Instance.toggle_SUGAR_ONLY,
				new GUIContent("Enable Secondary structures", "Switch between all-atoms and secondary structures representation"));
			if(!ssToggled && GUIMoleculeController.Instance.toggle_SUGAR_ONLY) { // enabling the ribbons
				Ribbons ribbons = new Ribbons();
				ribbons.CreateRibbons();
				GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;
			} else {
				if (ssToggled && !GUIMoleculeController.Instance.toggle_SUGAR_ONLY) { // destroying the ribbons
					GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;
					GameObject[] Objs = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];;
					foreach(GameObject ribObj in Objs){
						if(ribObj.name == "Ribbons")
						//foreach(GameObject ribObj in ribbonObjs)
							GameObject.Destroy(ribObj);
					}
				}
			}			
			GUILayout.EndHorizontal();

			// Bugs otherwise.
			if(!UIData.Instance.hasMoleculeDisplay) {
				GUIMoleculeController.Instance.showSecStructMenu = false;
				return;
			}
			
			int labelWidth = (int) (0.45f * Rectangles.secStructMenuWidth);
			int sliderWidth = (int) (0.50f * Rectangles.secStructMenuWidth);
			
			GUILayout.BeginHorizontal();
			RibbonsData.Instance.ribbonWidth[0] = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.ribbonWidth[0], 0.375f, 3.0f,
				"Helix Width: " + RibbonsData.Instance.ribbonWidth[0].ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();

		

			GUILayout.BeginHorizontal();
			RibbonsData.Instance.ribbonWidth[1] = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.ribbonWidth[1], 0.425f, 3.4f,
				"Sheet Width: " + RibbonsData.Instance.ribbonWidth[1].ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			RibbonsData.Instance.ribbonWidth[2] = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.ribbonWidth[2], 0.075f, 0.6f,
				"Coil Width: " + RibbonsData.Instance.ribbonWidth[2].ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			RibbonsData.Instance.THICKNESS = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.THICKNESS, 0.075f, 0.6f,
				"Thickness: " + RibbonsData.Instance.THICKNESS.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			RibbonsData.Instance.HELIX_DIAM = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.HELIX_DIAM, 0.45f, 3.6f,
				"Helix diameter: " + RibbonsData.Instance.HELIX_DIAM.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			RibbonsData.Instance.ARROW_WIDTH = GUIMoleculeController.Instance.LabelSlider(RibbonsData.Instance.ARROW_WIDTH, 0f, 3.6f,
				"Arrow width: " + RibbonsData.Instance.ARROW_WIDTH.ToString("0.00"), "", true, sliderWidth, labelWidth, true);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal ();
			//UIData.Instance.ssColStruct = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(UIData.Instance.ssColStruct,
			//new GUIContent("Color by Structure", "Color cartoon representation by structure"));
			if(GUILayout.Button(new GUIContent("Color by ss")))
				UIData.Instance.ssColStruct = !UIData.Instance.ssColStruct;
			GUILayout.EndHorizontal();

			if(UIData.Instance.ssColStruct){
			GUILayout.BeginHorizontal();
			GUILayout.Label("Helix Color :");
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(helixButton,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
				GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.HELIX_COLOR, "Helix color", null);
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Sheet Color :");
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(sheetButton,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
				GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.STRAND_COLOR, "Sheet color", null);
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Coil Color :");
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(coilButton,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
				GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.COIL_COLOR, "Coil color", null);
			}
			GUILayout.EndHorizontal();
			}

			//C.R Test color by chains
			GUILayout.BeginHorizontal ();
			//UIData.Instance.ssColChain = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(UIData.Instance.ssColChain,
			//new GUIContent("Color by Chain", "Color cartoon representation by chain"));
			if(GUILayout.Button(new GUIContent("Color by chain")))
				UIData.Instance.ssColChain = !UIData.Instance.ssColChain;
			GUILayout.EndHorizontal();

			if (UIData.Instance.ssColChain) {

				if(UIData.Instance.isGLIC){
					GUILayout.BeginHorizontal();
					UIData.Instance.ssDivCol = UIData.Instance.hasMoleculeDisplay && GUILayout.Toggle(UIData.Instance.ssDivCol,
					new GUIContent("Show domains", "Color by domains"));
					GUILayout.EndHorizontal();}

				GUILayout.BeginHorizontal();
				GUILayout.Label("Chain A :");
				if(GUILayout.Button(chainbuttonA,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
					GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.ChainColorA, "Chain A color", null);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Chain B :");
				if(GUILayout.Button(chainbuttonB,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
					GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.ChainColorB, "Chain B color", null);
				}
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Chain C :");
				if(GUILayout.Button(chainbuttonC,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
					GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.ChainColorC, "Chain C color", null);
				}
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Chain D :");
				if(GUILayout.Button(chainbuttonD,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
					GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.ChainColorD, "Chain D color", null);
				}
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Chain E :");
				if(GUILayout.Button(chainbuttonE,GUILayout.MinWidth(100),GUILayout.MinHeight(20))){
					GUIMoleculeController.Instance.CreateColorPicker(RibbonsData.Instance.ChainColorE, "Chain E color", null);
				}
				GUILayout.EndHorizontal();
			}
			
			GUILayout.BeginHorizontal();
			GUI.enabled = GUIMoleculeController.Instance.toggle_SUGAR_ONLY;
			if(GUILayout.Button(new GUIContent("Apply changes"))) {
				ApplySugarChanges();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			
			// ---------- C-ALPHA TRACE ----------
			GUILayout.BeginHorizontal();
			GUILayout.Box("C-alpha trace");
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			ChooseStructure();
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal();
			ChooseSmoothness();
			GUILayout.EndHorizontal ();	

			//C.R

			GUILayout.BeginHorizontal();
			GUILayout.Box("Bfactor Representation");
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			ChooseStructure_BF();
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal();
			ChooseSmoothness_BF();
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal();
			MinMaxChoice ();
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal();
			SetHighBFSlider ();
			GUILayout.EndHorizontal ();
			
			GUI.DragWindow();
		}
		
		
		/// <summary>
		/// Helper function to fill a texture choice menu with up to 15 boxes.
		/// </summary>
		/// <param name='texDir'>
		/// Texture directory.
		/// </param>
		/// <param name='texList'>
		/// List of textures.
		/// </param>
		/// <param name='texDescr'>
		/// Texture description.
		/// </param>
		private void textureMenu (string texDir, string[] texList, string texDescr) {
//			GUI.Label (new Rect (0, 0, 290, 20), "Surface Texture - " + texDescr, CentredText);
			GUIMoleculeController.Instance.showSurfaceTexture = SetTitleExit("Surface Texture - " + texDescr); //, CentredText);
			
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			UIData.Instance.grayscalemode = GUILayout.Toggle (UIData.Instance.grayscalemode, new GUIContent ("Grayscale", "Use grayscale version of the texture"));
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("<<", "Go to previous series of textures"))) { // cycle through texture sets 
				GUIMoleculeController.Instance.texture_set--; 
					
				if(GUIMoleculeController.Instance.onlyBestTextures){
					if(GUIMoleculeController.Instance.texture_set < 0)
						GUIMoleculeController.Instance.texture_set = 4; // First 5 pages are best textures (0-4)
				}
				else{
					if(GUIMoleculeController.Instance.texture_set < 5)
						GUIMoleculeController.Instance.texture_set = GUIDisplay.Instance.textureMenuList.Count - 1;
				}
			}			

//			if(GUILayout.Button(new GUIContent("Confirm","Confirm all the modification of the molecule.")))
			if (GUILayout.Button (new GUIContent ("Open", "Open custom texture image from disk"))) {	
				GUIMoleculeController.Instance.FileBrowser_show2 = true;
				GUIMoleculeController.Instance.m_fileBrowser = new ImprovedFileBrowser (
	                new Rect (400, 100, 600, 500),
	                "Choose Image File",
	                GUIMoleculeController.Instance.FileSelectedCallback,
	                GUIMoleculeController.Instance.m_last_extSurf_Path
	            );
			}
			
			if (GUILayout.Button (new GUIContent (">>", "Go to next series of textures"))) { // cycle through texture sets 
				GUIMoleculeController.Instance.texture_set++; 

				if (GUIMoleculeController.Instance.onlyBestTextures) {
					if(GUIMoleculeController.Instance.texture_set > 4) // First 5 pages are best textures (0-4)
						GUIMoleculeController.Instance.texture_set = 0;
				}
				else{
					if (GUIMoleculeController.Instance.texture_set > GUIDisplay.Instance.textureMenuList.Count - 1)
						GUIMoleculeController.Instance.texture_set = 5;
				}
			}			
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();			

			// check whether texList has more than 15 entries and raise an error!!
			int i = 0;
//			GUILayout.EndHorizontal(); GUILayout.Box(texDescr); GUILayout.BeginHorizontal();
			foreach (string texFil in texList) {
				i++; 
				if (i > 5) {
					GUILayout.EndHorizontal (); 
					GUILayout.BeginHorizontal (); 
					i = 1;
				}
//				if(GUILayout.Button((Texture2D)Resources.Load(texDir+texFil),GUILayout.Width(50),GUILayout.Height(50))) 
				int buttonWidth = (int) (Rectangles.textureWidth *0.18f);
				int buttonHeight = (int) (Rectangles.textureHeight / 4f);
				if (GUILayout.Button (new GUIContent ((Texture2D)Resources.Load (texDir + texFil), texFil), GUILayout.Width (buttonWidth), GUILayout.Height (buttonHeight))) { 
					if(texFil != "None") {
						GUIMoleculeController.Instance.surfaceTexture = true;
						GUIMoleculeController.Instance.externalSurfaceTexture = false;
						GUIMoleculeController.Instance.surfaceTextureDone = false;
						GUIMoleculeController.Instance.surfaceTextureName = texDir + texFil;
					}
					else {
						GUIMoleculeController.Instance.surfaceTexture = true;
						GUIMoleculeController.Instance.externalSurfaceTexture = false;
						GUIMoleculeController.Instance.buildSurfaceDone = false;
						GUIMoleculeController.Instance.surfaceTextureDone = false;
						GUIMoleculeController.Instance.surfaceTextureName = "lit_spheres/divers/daphz1";
					}
				}	
			}
			GUILayout.EndHorizontal ();
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}
		
		
		/// <summary>
		/// Defines the texture selection window.
		/// Negative values of GUIMoleculeController.Instance.texture_set are used to represent the "best" sets.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void SurfaceTexture (int a) {
			
			textureMenu ("lit_spheres/", GUIDisplay.Instance.textureMenuList[ GUIMoleculeController.Instance.texture_set], GUIDisplay.Instance.textureMenuTitles[ GUIMoleculeController.Instance.texture_set]);
			
			GUI.DragWindow();
		}	// End of SurfaceTexture				
		
		
		/// <summary>
		/// Defines the rendering parameters, in the atom appearance menu.
		/// </summary>
		private void RenderingParameters () {
//			toggle_HB_SANIM = GUILayout.Toggle (toggle_HB_SANIM, new GUIContent ("HB_SANIM", "Animate shrink parameter"));
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Color/Texture/Scale"); // Make a little more place between the representation selection and the rendering parameters
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace(); // so as to center the buttons
			if (GUILayout.Button (new GUIContent ("Renderer", "Choose the color, texture and scale of each atom"), GUILayout.Width(Rectangles.atomButtonWidth))){
				GUIMoleculeController.Instance.showSetAtomScales = !GUIMoleculeController.Instance.showSetAtomScales;
				GUIMoleculeController.Instance.showAtomsExtendedMenu = false;
				GUIMoleculeController.Instance.showResiduesMenu = false;
				GUIMoleculeController.Instance.showChainsMenu = false;
				GUIDisplay.Instance.applyToAtoms.Add("All");
			}
			
			if (GUILayout.Button (new GUIContent ("Panels", "Open colors and textures panels menu"), GUILayout.Width(Rectangles.atomButtonWidth))) {
				GUIMoleculeController.Instance.showPanelsMenu = !GUIMoleculeController.Instance.showPanelsMenu;
			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal();
			GUI.enabled = true;
			GUIMoleculeController.Instance.toggle_NA_HIDE = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_NA_HIDE, new GUIContent ("Hide", "Hide/Display atoms")); // && !carbon_alpha?
			GUILayout.EndHorizontal();
			
			GUIMoleculeController.Instance.globalRadius = GUIMoleculeController.Instance.LabelSlider (GUIMoleculeController.Instance.globalRadius, 0.00001f, 2.0f, "Radius " + GUIMoleculeController.Instance.globalRadius.ToString("0.00"), "Determines radius value", true, (int)(0.90 * Rectangles.atomMenuWidth), 1, true);
			
			
			if (GUIMoleculeController.Instance.toggle_NA_HBALLSMOOTH) {
				GUIMoleculeController.Instance.m_colorPicker = null;
				UIData.Instance.resetDisplay = true;
				UIData.Instance.isCubeToSphere = false;
				UIData.Instance.isSphereToCube = true;
				
				UIData.Instance.atomtype = UIData.AtomType.hyperball;
				Debug.Log ("UIData.Instance.resetDisplay :: " + UIData.Instance.resetDisplay);
				Debug.Log ("UIData.Instance.isCubeToSphere :: " + UIData.Instance.isCubeToSphere);
				Debug.Log ("UIData.Instance.isSphereToCube :: " + UIData.Instance.isSphereToCube);
				GUIMoleculeController.Instance.showAtomType = false;
				
				BallUpdate.resetColors = true;
				BallUpdate.resetRadii = true;
				UIData.Instance.resetBondDisplay = true;
				UIData.Instance.bondtype = UIData.BondType.hyperstick;
				GUIMoleculeController.Instance.showBondType = false;
				
				GUIMoleculeController.Instance.globalRadius = 0.4f;
				GUIMoleculeController.Instance.shrink = 0.5f;
				GUIMoleculeController.Instance.linkScale = 1.0f;
				
				GUIMoleculeController.Instance.toggle_NA_HBALLSMOOTH = false;
			}
			

			GUILayout.BeginHorizontal();
			if(GUIMoleculeController.Instance.toggle_NA_HIDE || UIData.Instance.atomtype == UIData.AtomType.particleball)
				GUI.enabled = false;
			toggle_NA_SWITCH = GUILayout.Toggle (toggle_NA_SWITCH, new GUIContent 
				("LOD mode", "Toggle LOD.  When this is enabled and the molecule is moving, hyperboloids are replaced by particle balls for smoother framerates."));
			UIData.Instance.switchmode = toggle_NA_SWITCH;
			GUI.enabled = true;
			if(GUIMoleculeController.Instance.toggle_NA_HIDE)
				GUI.enabled = false;
			GUIMoleculeController.Instance.toggle_NA_AUTOMOVE = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_NA_AUTOMOVE, new GUIContent ("Automove", "Camera auto rotation"));
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			
			GUI.enabled = true;
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;

		}
		
		private string structTypeButtonLabel(string st) {
			if(st == "All atoms")
				return("C-alpha trace");
			else
				return("All atoms");
		}

		private string structTypeButtonLabel_BF(string st) {
			if(st == "All atoms")
				return("B Factor");
			else
				return("All atoms");
		}		
		
		/// <summary>
		/// Induces switch between all-atom and carbon alpha trace representations, as necessary.
		/// </summary>
		private void ChooseStructure () {
			GUILayout.BeginHorizontal ();
			
			GUI.enabled = (MoleculeModel.CatomsLocationlist.Count > 2);
			if (GUILayout.Button (new GUIContent (structTypeButtonLabel(GUIMoleculeController.Instance.structType), "Switch to " + structTypeButtonLabel(GUIMoleculeController.Instance.structType)))) {
				// Luiz:
				SwitchStruct(false);
				SetAtomStyle();
			}

			GUI.enabled = true;
			GUILayout.EndHorizontal ();
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}

		/// <summary>
		/// Induces switch between all-atom and Bfactor color/radius representation
		/// </summary>
		private void ChooseStructure_BF () {
			GUILayout.BeginHorizontal ();
			
			GUI.enabled = (MoleculeModel.CatomsLocationlist.Count > 2);
			if (GUILayout.Button (new GUIContent (structTypeButtonLabel_BF(GUIMoleculeController.Instance.structType), "Switch to " + structTypeButtonLabel_BF(GUIMoleculeController.Instance.structType)))) {
				// Luiz:
				SwitchStruct(true);
				SetAtomStyle();
			}
			GUI.enabled = true;
			GUILayout.EndHorizontal ();
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}

		/// <summary>
		/// Chooses the smoothness of the carbon alpha trace(s).
		/// </summary>
		private void ChooseSmoothness () {
			bool isChain = (GUIMoleculeController.Instance.structType == "C-alpha trace");
			int labelWidth = (int) (0.35f * Rectangles.secStructMenuWidth);
			int sliderWidth = (int) (0.55f * Rectangles.secStructMenuWidth);
			int newSmooth;
			newSmooth = (int) GUIMoleculeController.Instance.LabelSlider(GenInterpolationPoint.smoothnessFactor, 1f, 15f,
					"Smoothness", "Smoothness of the carbon alpha chain spline", isChain, sliderWidth, labelWidth, true);
			GUI.enabled = true;
			
			if(newSmooth != GenInterpolationPoint.smoothnessFactor) {
				ChangeSmoothness(newSmooth);
			}
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		} // End of ChooseSmoothness

		/// <summary>
		/// Chooses the smoothness of the BFactor color/radius Representation.
		/// </summary>
		private void ChooseSmoothness_BF () {
			bool isChain = (GUIMoleculeController.Instance.structType == "B Factor");
			int labelWidth = (int) (0.35f * Rectangles.secStructMenuWidth);
			int sliderWidth = (int) (0.55f * Rectangles.secStructMenuWidth);
			int newSmooth;
			newSmooth = (int) GUIMoleculeController.Instance.LabelSlider(GenInterpolationPoint_BF.smoothnessFactor, 1f, 15f,
			                              "Smoothness", "Smoothness of the Bfactor color/radius Representation", isChain, sliderWidth, labelWidth, true);
			GUI.enabled = true;
			
			if(newSmooth != GenInterpolationPoint_BF.smoothnessFactor) {
				ChangeSmoothnessBF(newSmooth);
			}
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		} // End of ChooseSmoothness

		/// <summary>
		/// Display slider setting the radius value for the highest Bfactor
		/// </summary>
		private void SetHighBFSlider(){

			bool isChain = (GUIMoleculeController.Instance.structType == "B Factor");
			int labelWidth = (int) (0.35f * Rectangles.secStructMenuWidth);
			int sliderWidth = (int) (0.55f * Rectangles.secStructMenuWidth);

			GUI.enabled = true;

			GUIMoleculeController.Instance.highBFradius = GUIMoleculeController.Instance.LabelSlider(GUIMoleculeController.Instance.highBFradius, 1.0f, 2.0f, "High value radius", "Set highest Bfactor radius value", isChain, sliderWidth, labelWidth, true);
			
			if (GUI.changed) {
				BallUpdate.resetRadii = true;
			}
		}

		/// <summary>
		/// Chooses min and max values to use for BFactor Representation
		/// </summary>
		public void MinMaxChoice(){

			bool isChain = (GUIMoleculeController.Instance.structType == "B Factor");
			int textWidth = (int) (0.18f * Rectangles.secStructMenuWidth);
			int buttonWidth = (int) (0.47 * Rectangles.secStructMenuWidth);
			GUI.enabled = isChain;

			GUILayout.Label ("Choose scale");
			GUILayout.Label ("Min");
			//BFactorRep.Instance.minval = GUILayout.TextField (BFactorRep.Instance.minValue.ToString(), 8, GUILayout.Width (labelWidth));
			BFactorRep.Instance.minval = GUILayout.TextField (BFactorRep.Instance.minval, 6, GUILayout.Width (textWidth));
			GUILayout.Label ("Max");
			BFactorRep.Instance.maxval = GUILayout.TextField (BFactorRep.Instance.maxval, 6, GUILayout.Width (textWidth));

			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();

			if (GUILayout.Button (new GUIContent ("Rescaling", "Rescale Bfactor values"), GUILayout.Width (buttonWidth))) {
				Rescaling();
			}

			if (GUILayout.Button (new GUIContent ("Reset", "Reset to original Bfactor values"), GUILayout.Width (buttonWidth))){
				Reset();
			}
		}
			
/*
		private string hideOrShowAtoms() {
			if (showAtoms)
				return("Hide atoms");
			else
				return("Show atoms");
		}
		
		
		/// <summary>
		/// Toggles the atoms (by modifying their radius, which is less than ideal).
		/// </summary>
		private void ToggleAtoms() {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(new GUIContent(hideOrShowAtoms(), "This button enables/disables atoms"))) {
				showAtoms = !showAtoms;
				if(showAtoms)
					globalRadius = prevRadius ;
				else {
					prevRadius = globalRadius ;
					globalRadius = 0f;
				}
			}
			GUILayout.EndHorizontal();
		} // End of ToggleAtoms
*/
		
		
		/// <summary>
		/// Sets the atom style. Calls a few sub-functions that define GUI elements for structure, smoothness, etc.
		/// </summary>
		private void SetAtomStyle () {			
			
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();
			GUILayout.Label("Atom Style", GUILayout.Width(Rectangles.atomButtonWidth));
			GUILayout.Label("Bond Style", GUILayout.Width(Rectangles.atomButtonWidth));
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();
			string atomtype = "";
			UIData.AtomType atype = UIData.Instance.atomtype;
			if(atype == UIData.AtomType.noatom)
				atype = GUIMoleculeController.Instance.Molecule3DComp.PreviousAtomType;
			switch (atype) {
			case UIData.AtomType.cube:
				atomtype = "Cube";
				break;
			case UIData.AtomType.sphere:	
				atomtype = "Sphere";
				break;
			case UIData.AtomType.hyperball:
				atomtype = "HyperBall";
				break;
			case UIData.AtomType.raycasting:
				atomtype = "Raycasting";
				break;
			case UIData.AtomType.billboard:
				atomtype = "Common Billboard";
				break;
			case UIData.AtomType.rcbillboard:
				atomtype = "RayCasting Billboard";
				break;
			case UIData.AtomType.hbbillboard:
				atomtype = "HyperBall Billboard";
				break;
			case UIData.AtomType.rcsprite:
				atomtype = "RayCasting Sprite";
				break;
			case UIData.AtomType.multihyperball:
				atomtype = "Multi-Hyperball";
				break;
			case UIData.AtomType.combinemeshball:
				atomtype = "CombineMesh HyperBall";
				break;
			case UIData.AtomType.particleball:
				atomtype = "ParticleBall";
				break;
			case UIData.AtomType.particleballalphablend:
				atomtype = "ParticleBallAlpahBlend";
				break;
			}

			string displayAtomType;
			if (atomtype == "ParticleBall")
				displayAtomType = "Particle";
			else
				displayAtomType = atomtype;
			if (GUILayout.Button (new GUIContent (displayAtomType, "Change the atom appearance style or rendering method"), GUILayout.Width(Rectangles.atomButtonWidth))) {
				GUIMoleculeController.Instance.showAtomType = !GUIMoleculeController.Instance.showAtomType;
				GUIMoleculeController.Instance.showBondType = false;
				GUIMoleculeController.Instance.m_colorPicker = null;
			}


			string bondtype = "";
			switch (UIData.Instance.bondtype) {
			case UIData.BondType.cube:
				bondtype = "Cube";
				break;
			case UIData.BondType.line:
				bondtype = "Line";
				break;
			case UIData.BondType.hyperstick:
				bondtype = "HyperStick";
				break;
			case UIData.BondType.tubestick:
				bondtype = "TubeStick";
				break;
			case UIData.BondType.bbhyperstick:
				bondtype = "Billboard HyperStick";
				break;
			case UIData.BondType.particlestick:
				bondtype = "Particle Stick";
				break;
			case UIData.BondType.nobond:
				bondtype = "No Stick";
				break;
			}

			
			if (GUILayout.Button (new GUIContent (bondtype, "Change the bond appearance style or rendering method"), GUILayout.Width(Rectangles.atomButtonWidth))) {	
				GUIMoleculeController.Instance.showBondType = !GUIMoleculeController.Instance.showBondType;
				GUIMoleculeController.Instance.showAtomType = false;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal();
			if(GUIMoleculeController.Instance.toggle_NA_HIDE)
				GUI.enabled = false;
			if(GUILayout.Button(new GUIContent("Smooth HyperBalls", "Set a parameter combo for HyperBalls and Sticks with SmoothLinks once"))) {
				SmoothHyperBalls ();
			}
			GUILayout.EndHorizontal();
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		} // End of SetAtomStyle
			
		
		/// <summary>
		/// Defines the Atom menu.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void AtomMenu (int a) {
			GUIMoleculeController.Instance.showAtomMenu = SetTitleExit("Atom appearance");
			SetAtomStyle ();
			RenderingParameters ();
			
			GUILayout.BeginHorizontal();
			GUIMoleculeController.Instance.toggle_NA_CLICK = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_NA_CLICK, new GUIContent ("Atom selection", "Toggles mouse clicking to select/deselect atoms (left click/right click)"));
			GUILayout.EndHorizontal();

			// Luiz FIXME gotta find a way yo synchronize selection
			GUIMoleculeController.Instance.toggle_NA_CLICK = false;
			
			GUILayout.BeginHorizontal();
			GUIMoleculeController.Instance.toggle_NA_CAMLOCK = GUILayout.Toggle (GUIMoleculeController.Instance.toggle_NA_CAMLOCK, new GUIContent ("Lock camera", "Enable/Disable camera movements"));
			GUILayout.EndHorizontal();
			
			if (!GUIMoleculeController.Instance.toggle_NA_CLICK && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ClickAtom> ())
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ClickAtom> ().enabled = false;
			else if (GUIMoleculeController.Instance.toggle_NA_CLICK && GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ClickAtom> ())
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ClickAtom> ().enabled = true;
			
			
			int sliderWidth = (int) (Rectangles.atomMenuWidth * 0.5f);
			int labelWidth = (int) (Rectangles.atomButtonWidth * 0.4f);
			
			GUILayout.BeginHorizontal();
			HBallManager.brightness = GUIMoleculeController.Instance.LabelSlider(HBallManager.brightness, 0.33f, 2.0f, "Brightness: " + HBallManager.brightness.ToString("0.00"), 
									"Adjusts the brightness of atoms and bonds represented with the MatCap shader",
									(UIData.Instance.atomtype == UIData.AtomType.hyperball), sliderWidth, labelWidth, false);
			if(GUI.changed)
				HBallManager.resetBrightness = true;
			GUILayout.EndHorizontal();
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			GUI.DragWindow();
		} // End of AtomMenu		
		
		
		/// <summary>
		/// Loads the GUI components for taking screenshots in the display window.
		/// </summary>
		private void LoadScreenShot () {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button (new GUIContent ("ScreenShot", "Capture the screen and save image to the original file path"))) {
				GameObject LocCamera = GameObject.Find ("Camera");
				ScreenShot comp = LocCamera.GetComponent<ScreenShot> ();
				comp.open = true;
			}

			GUILayout.EndHorizontal();
			//////////modify///////////////////////
						
			if (GUILayout.Button (new GUIContent ("ScreenShot Sequence", "Capture the screen sequentially and save images to the original file path"))) {
				GameObject LocCamera = GameObject.Find ("Camera");
				ScreenShot comp = LocCamera.GetComponent<ScreenShot> ();
				comp.sequence = !comp.sequence;
				comp.open = !comp.sequence;				
			}	
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		} // End of LoadScreenShot
		

		
		/// <summary>
		/// Defines the GUI components that allow for background color control in the display menu.
		/// </summary>
		private void BackGroundControl () {
			GUILayout.BeginHorizontal ();
			
			GUILayout.Label (new GUIContent ("BackGround", "Toggle the use of a skybox on/off"), GUILayout.MaxWidth (120));
			
			UIData.Instance.backGroundIs = GUILayout.Toggle (UIData.Instance.backGroundIs, new GUIContent ("Yes", "Toggle the use of a skybox to ON"));
			UIData.Instance.backGroundNo = !UIData.Instance.backGroundIs;

			UIData.Instance.backGroundNo = GUILayout.Toggle (UIData.Instance.backGroundNo, new GUIContent ("No", "Toggle the use of a skybox to OFF"));
			UIData.Instance.backGroundIs = !UIData.Instance.backGroundNo;
			
			GUILayout.EndHorizontal ();
			
			// MB: only show possibility to change skybox if it is set to on
			GUIMoleculeController.Instance.showBackgroundType = UIData.Instance.backGroundIs ;

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
		}
		

		/// <summary>
		/// Defines the GUI components for setting the BackGround color. Part of the Display window.
		/// </summary>
		private void BackColor () {
			//Luiz: 
			GUILayout.BeginHorizontal ();

			Color newColor = sNullColor;

			if (GUILayout.Button (new GUIContent ("White", "Set background to plain white"))) {
				// Luiz:
				newColor = new Color (1, 1, 1, 0);
			}
			
			if (GUILayout.Button (new GUIContent ("Grey", "Set background color to grey"))) {
				// Luiz:
				newColor = Color.gray;
			}

			if (GUILayout.Button (new GUIContent ("Black", "Set background color to plain black"))) {
				// Luiz:
				newColor = Color.black;
			}

			// Luiz:
			ChangeBackgroundColor (new ColorPicker.ColorEventArgs(newColor, null, null, null));

			GUILayout.EndHorizontal ();

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;

			// Luiz:
//			GUILayout.BeginHorizontal ();
//			if (GUILayout.Button (new GUIContent ("Background Color", "Choose the background color"))) {
//				if (GUIMoleculeController.Instance.m_colorPicker != null)
//					GUIMoleculeController.Instance.m_colorPicker = null;
//					
//				GUIMoleculeController.Instance.m_colorPicker = new ColorPicker(Rectangles.colorPickerRect, GUIMoleculeController.Instance.BackgroundColor, null, "All", "All", "Background Color");
//			}
//			GUILayout.EndHorizontal ();
		}
		
		
		
		/// <summary>
		/// Defines the Display window, opened from the maim menu.
		/// </summary>
		/// <param name='a'>
		/// A.
		/// </param>
		public void Display (int a) {
			GUIMoleculeController.Instance.showManipulatorMenu = SetTitleExit("Display");
			// VisualControl ();
			LoadScreenShot ();
			//ShaderControl();
			BackGroundControl ();

			if(GUIMoleculeController.Instance.showBackgroundType)
				GUI.enabled = false;
			BackColor ();
			GUI.enabled = true;
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Effects", "Toggle what kind of special effect to apply to the scene"))) 
				GUIMoleculeController.Instance.showEffectType = !GUIMoleculeController.Instance.showEffectType;
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (new GUIContent ("Infos", "Show/Hide the FPS, atom count and bond count"))) 
				GUIMoleculeController.Instance.toggle_INFOS = !GUIMoleculeController.Instance.toggle_INFOS;
			GUILayout.EndHorizontal ();

			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			GUI.DragWindow();
		} // End of Display

		// Luiz:
		public void Energy(int id) {
			EnergyWindow.Draw(
				TrajectoryData.Instance.CurrentState.Energy,
				TrajectoryData.Instance.StateEnergyMinMax.max,
				TrajectoryData.Instance.StateEnergyMinMax.min
			);

			GUI.DragWindow();
		}
		
		/// <summary>
		/// Defines the manipulator window, labaled "Movement" in the program.
		/// Opened by default when a molecule is loaded.
		/// </summary>
		/// <param name='a'>
		/// Window identifier.
		/// </param>
		public void Manipulator (int a) {
			SetTitle("Molecule Manipulator");
			maxCamera fixeCam;
			fixeCam = GUIMoleculeController.Instance.scenecontroller.GetComponent<maxCamera> ();
			if (GUILayout.RepeatButton (new GUIContent ("Up", "Move Up")))
				fixeCam.upyDeg ();
			
			GUILayout.BeginHorizontal ();
			if (GUILayout.RepeatButton (new GUIContent ("Left", "Move Left")))
				fixeCam.downxDeg ();

			if (GUILayout.RepeatButton (new GUIContent ("Right", "Move Right")))
				fixeCam.upxDeg ();
			
			GUILayout.EndHorizontal ();

			if (GUILayout.RepeatButton (new GUIContent ("Down", "Move Down")))
				fixeCam.downyDeg ();

			GUILayout.BeginHorizontal ();
			if (GUILayout.RepeatButton (new GUIContent ("Rot Left", "Rotate Left")))
				fixeCam.upzDeg ();

			if (GUILayout.RepeatButton (new GUIContent ("Rot Right", "Rotate Right")))
				fixeCam.downzDeg ();

			GUILayout.EndHorizontal ();

			if (TrajectoryData.Instance.IsLoaded) {
				var oldAutoChangeState = UIData.Instance.autoChangingState;
				GUILayout.BeginHorizontal();
				UIData.Instance.autoChangingState = GUILayout.Toggle(
					UIData.Instance.autoChangingState, new GUIContent ("Auto state transition", "Automatically transitions between states.")
				);
				GUILayout.EndHorizontal();

				if(!oldAutoChangeState && UIData.Instance.autoChangingState) {
					TrajectoryData.Instance.GoToNextState();
				}

				TrajectoryData.Instance.CurrentStateIdx = (int) System.Math.Round(GUIMoleculeController.Instance.LabelSlider(
					TrajectoryData.Instance.CurrentStateIdx,
					0f,
					TrajectoryData.Instance.NumberOfStates - 1,
					"State: " + (TrajectoryData.Instance.CurrentStateIdx + 1) + " / " + (TrajectoryData.Instance.NumberOfStates),
					"State of molecule across time",
					!UIData.Instance.autoChangingState,
					(int)(0.9f * Rectangles.manipulatorWidth),
					100,
					true
				));
			}
			
			if (Event.current.type == EventType.Repaint)
				MoleculeModel.newtooltip = GUI.tooltip;
			GUI.DragWindow();
		} // End of Manipulator	

		// Luiz:
		private void DispatchMethodPerformedEvent(string methodName, object param)
		{
			ChangeManager.DispatchMethodEvent (typeof(LoadTypeGUI), methodName, param);
		}
		public void SwitchStruct(bool isBF) {
			if (isBF) {
				if (UIData.Instance.secondarystruct) {
					UIData.Instance.secondarystruct = false;
					GUIMoleculeController.Instance.structType = "All atoms";
					UIData.Instance.changeStructure = true;
					GUIMoleculeController.Instance.globalRadius = 0.40f;
					GUIMoleculeController.Instance.shrink = 0.50f;
				} else {
					UIData.Instance.secondarystruct = true;
					GUIMoleculeController.Instance.structType = "B Factor";
					//	DisplayMolecule.DestroyAtomsAndBonds();
					UIData.Instance.toggle_bf = true;
					BFactorRep.Instance.CreateBFRep();
					DisplayMolecule.InitManagers();
					UIData.Instance.changeStructure = true;
					GUIMoleculeController.Instance.globalRadius = 0.15f;
					GUIMoleculeController.Instance.shrink = 0.0001f;

					//	UIData.Instance.resetDisplay = true;
				}

				//if (Event.current.type == EventType.Repaint)
				//MoleculeModel.newtooltip = GUI.tooltip;
				DisplayMolecule.DestroyAtomsAndBonds();
				UIData.Instance.resetDisplay = true ;
			} else {
				if (UIData.Instance.secondarystruct) {
					UIData.Instance.secondarystruct = false;
					GUIMoleculeController.Instance.structType = "All atoms";
					UIData.Instance.changeStructure = true;
					GUIMoleculeController.Instance.globalRadius = 0.40f;
					GUIMoleculeController.Instance.shrink = 0.50f;
				} else {
					UIData.Instance.secondarystruct = true;
					GUIMoleculeController.Instance.structType = "C-alpha trace";
					if (UIData.Instance.toggle_bf){
						AlphaChainSmoother.ReSpline ();
						DisplayMolecule.InitManagers();
						UIData.Instance.toggle_bf = false;
					}
					UIData.Instance.changeStructure = true;
					GUIMoleculeController.Instance.globalRadius = 0.25f;
					GUIMoleculeController.Instance.shrink = 0.0001f;
				}
				DisplayMolecule.DestroyAtomsAndBonds();
				UIData.Instance.resetDisplay = true ;
			}

			DispatchMethodPerformedEvent("SwitchStruct", isBF);
		}
		public void ChangeSmoothness(int newSmooth) {
			GenInterpolationPoint.smoothnessFactor = newSmooth;
			DisplayMolecule.DestroyAtomsAndBonds();
			AlphaChainSmoother.ReSpline();
			DisplayMolecule.InitManagers();
			UIData.Instance.changeStructure = true;
			UIData.Instance.resetDisplay = true;

			DispatchMethodPerformedEvent("ChangeSmoothness", newSmooth);
		}
		public void ChangeSmoothnessBF(int newSmooth) {
			GenInterpolationPoint_BF.smoothnessFactor = newSmooth;
			DisplayMolecule.DestroyAtomsAndBonds();
			BFactorRep.Instance.CreateBFRep();
			DisplayMolecule.InitManagers();
			UIData.Instance.changeStructure = true;
			UIData.Instance.resetDisplay = true;

			DispatchMethodPerformedEvent("ChangeSmoothnessBF", newSmooth);
		}
		public void Rescaling() {
			UIData.Instance.isRescale = true;
			DisplayMolecule.DestroyAtomsAndBonds();
			BFactorRep.Instance.CreateBFRep();
			DisplayMolecule.InitManagers ();
			UIData.Instance.changeStructure = true;
			UIData.Instance.resetDisplay = true;

			DispatchMethodPerformedEvent("Rescaling", null);
		}
		public void Reset() {
			UIData.Instance.isRescale = false;
			DisplayMolecule.DestroyAtomsAndBonds();
			BFactorRep.Instance.CreateBFRep();
			DisplayMolecule.InitManagers ();
			UIData.Instance.changeStructure = true;
			UIData.Instance.resetDisplay = true;

			DispatchMethodPerformedEvent("Reset", null);
		}
		public void ApplySugarChanges() {
			// Destroying the ribbons
			GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;
			GameObject[] Objs = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];;
			foreach(GameObject ribObj in Objs){
				if(ribObj.name == "Ribbons")
					//foreach(GameObject ribObj in ribbonObjs)
					GameObject.Destroy(ribObj);
			}
			// Recreating them
			Ribbons ribbons = new Ribbons();
			ribbons.CreateRibbons();
			GUIMoleculeController.Instance.toggle_NA_HIDE = !GUIMoleculeController.Instance.toggle_NA_HIDE;

			DispatchMethodPerformedEvent("ApplySugarChanges", null);
		}
		public void ChangeRepresentation(UIData.AtomType type)
		{
			UIData.Instance.resetDisplay = true;
			UIData.Instance.atomtype = type;
			Debug.Log ("UIData.Instance.resetDisplay:" + UIData.Instance.resetDisplay);
			Debug.Log ("UIData.Instance.isCubeToSphere:" + UIData.Instance.isCubeToSphere);
			Debug.Log ("UIData.Instance.isSphereToCube:" + UIData.Instance.isSphereToCube);
			GUIMoleculeController.Instance.showAtomType = false;
			GUIMoleculeController.Instance.toggle_NA_HIDE = false;
			GUIMoleculeController.Instance.toggle_NA_CLICK = false;

			switch (UIData.Instance.atomtype) {
			case UIData.AtomType.cube:
				Debug.Log("Cube representation");
				UIData.Instance.isCubeToSphere = false;
				UIData.Instance.isSphereToCube = true;
				BallUpdate.resetColors = true;
				break;

			case UIData.AtomType.hyperball:
				Debug.Log("HyperBall representation");
				UIData.Instance.isCubeToSphere = false;
				UIData.Instance.isSphereToCube = true;
				BallUpdate.resetColors = true;
				break;

			case UIData.AtomType.particleball:
				Debug.Log("Particle representation");
				UIData.Instance.isSphereToCube = false;
				UIData.Instance.isCubeToSphere = false;
				UIData.Instance.resetBondDisplay = true;
				UIData.Instance.bondtype=UIData.BondType.nobond; // Probably best to do this by default. Users can still enable bonds if they wish.
				toggle_NA_SWITCH = false;
				GameObject shurikenParticleManagerObj = GameObject.FindGameObjectWithTag("ShurikenParticleManager");
				ShurikenParticleManager shManager = shurikenParticleManagerObj.GetComponent<ShurikenParticleManager>();
				//				shManager.Init();
				shManager.EnableRenderers();
				break;

			case UIData.AtomType.sphere:
				Debug.Log("Sphere representation");
				UIData.Instance.isCubeToSphere = true;
				UIData.Instance.isSphereToCube = false;
				BallUpdate.resetColors = true;
				break;
			}

			DispatchMethodPerformedEvent ("ChangeRepresentation", type);
		}
		public void ChangeBond(UIData.BondType type)
		{
			UIData.Instance.resetBondDisplay = true;
			UIData.Instance.bondtype = type;
			GUIMoleculeController.Instance.showBondType = false;

			DispatchMethodPerformedEvent ("ChangeBond", type);
		}
		public void SmoothHyperBalls()
		{
			GUIMoleculeController.Instance.toggle_NA_HBALLSMOOTH = !GUIMoleculeController.Instance.toggle_NA_HBALLSMOOTH;

			DispatchMethodPerformedEvent ("SmoothHyperBalls", null);
		}
		public void ChangeBackgroundColor(ColorPicker.ColorEventArgs e)
		{
			// Luiz: could be a property change, indeed, but god knows why JsonUtility can't 
			// serialize ColorObject correctly.

			var newColor = e.Color;
			if (newColor != sNullColor) {
				GUIMoleculeController.Instance.BackgroundColor = new ColorObject (newColor);
						Camera.main.backgroundColor = newColor;
				DispatchMethodPerformedEvent ("ChangeBackgroundColor", e);
			}
		}
		private Color sNullColor = new Color (0, 0, 0, 0);
	}
}
