/// @file MoleculeModel.cs
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
/// $Id: MoleculeModel.cs 660 2014-08-26 13:46:34Z sebastien $
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

namespace Molecule.Model {	
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using Config;

	public class MoleculeModel : MonoBehaviour {
		public static string sequence = "";
			
		/// <summary>
		/// The coordinates of each atom. List of float[3].
		/// </summary>
		public static List<float[]> atomsLocationlist = new List<float[]>();
		
		/// <summary>
		/// The coordinates of each atom, simulated through MDDriver. List of float[3].
		/// </summary>
		public static List<Vector3> atomsMDDriverLocationlist;
		
		/// <summary>
		/// The coordinates of each Carbon alpha. List of float[3].
		/// </summary>
		public static List<float[]> CatomsLocationlist = new List<float[]>();

		/// <summary>
		/// Backup of the coordinates of each Carbon alpha. List of float[3].
		/// </summary>
		public static List<float[]> backupCatomsLocationlist = new List<float[]>();
		
		/// <summary>
		/// The coordinates of each Carbon alpha in the CA-Spline. List of float[3].
		/// </summary>
		public static List<float[]> CaSplineList = new List<float[]>();
		
		/// <summary>
		/// The type of each atom. List of AtomModel.
		/// </summary>
		public static List<AtomModel> atomsTypelist = new List<AtomModel>();
		
		/// <summary>
		/// The name of each atom. E.g.: O, N, C, H1, H2, etc. List of strings.
		/// </summary>
		public static List<string> atomsNamelist = new List<string>();


		/// <summary>
		/// The number of each atoms (in the PDB file)
		/// </summary>
		public static List<int> atomsNumberList = new List<int>();

		public static List<string> atomsSugarNamelist = new List<string>();
		public static List<string> atomsSugarResnamelist = new List<string>();
		public static List<string> sugarResname = new List<string> {"ABE","ACE","ALT","API","ARA","DHA","FRU","FUC","GAL",
			"GLC","GUL","IDO","DKN","KDO","MAN","NEG","RHA","RIB","SIA","TAG","TAL","XYL",
			"GLA","FUL","GLB","NAG","NDG","BMA","MMA","A2G","AAL","BGC"};
		public static List<float[]> atomsSugarLocationlist = new List<float[]>();
		public static List<string> resSugarChainList = new List<string>();
		public static List<int[]> bondEPSugarList = new List<int[]>(); // Not sure of what EP means btw
		public static List<AtomModel> atomsSugarTypelist = new List<AtomModel>();
		public static List<int> sortedResIndexByListSugar = new List<int>();

		public static List<int[]> BondListFromPDB = new List<int[]>();

		public static List<string> atomHetTypeList = new List<string>();


		/// <summary>
		/// List of the names existing in the molecule.
		/// </summary>
		public static List<string> existingName = new List<string>();
		
		/// <summary>
		/// The name of the residue to which each atom belongs. E.g.: ALA, LEU, ASP, etc. List of strings.
		/// </summary>
		public static List<string> atomsResnamelist = new List<string>();
		
		/// <summary>
		/// List of the residues existing in the molecule.
		/// </summary>
		public static List<string> existingRes = new List<string>();
			
		/// <summary>
		/// The residue identifiers. One per atom.
		/// </summary>
		public static List<int> residueIds = new List<int>();
		
		/// <summary>
		/// The residues. Keys: residue IDs. Values: list of atoms IDs.
		/// </summary>
		public static Dictionary<int, ArrayList> residues = new Dictionary<int, ArrayList>();
		
		/// <summary>
		/// The chain of each atom.
		/// </summary>
		public static List<string> atomsChainList = new List<string>();

		/// <summary>
		/// The chain of each residue (only work if residues are numbered by chain).
		/// </summary>
		public static List<string> resChainList = new List<string>();

		/// <summary>
		/// The chain of each residue.
		/// </summary>
		public static List<string> resChainList2 = new List<string>();

		/// <summary>
		/// First residue number in pdb.
		/// </summary>
		public static int firstresnb = new int();

		/// <summary>
		/// List of the chains existing in the molecule.
		/// </summary>
		public static List<string> existingChain = new List<string>();
		
		/// <summary>
		/// The color of each atom.
		/// </summary>
		public static List<Color> atomsColorList = new List<Color>();

		public static List<float> atomsLocalScaleList = new List<float>();

		/// <summary>
		/// Terminal residue number of each subunits. 
		/// </summary>
		public static List<int> splits = new List<int>();
		
		/// <summary>
		/// Not used anymore.
		/// The bonds between atoms. Each element of this list is an int[2] where:
		/// int[0] is the index of the first atom,
		/// int[1] is the second one.
		/// </summary>
		public static List<int[]> bondList = new List<int[]>();

		/// <summary>
		/// The bonds between atoms. Each element of this list is an int[2] where:
		/// int[0] is the index of the first atom,
		/// int[1] is the second one.
		/// </summary>
		public static List<int[]> bondEPList = new List<int[]>(); // Not sure of what EP means btw

		/// <summary>
		/// Dictionary of every bounded atoms (which atoms whith which atoms).
		/// </summary>
		public static Dictionary<int, List<int>> bondEPDict= new Dictionary<int, List<int>>();

		public static List<int[]> CSidList = new List<int[]>(); // List of IDs for networks, or something like that.
		public static List<string[]> CSSGDList = new List<string[]>(); // Dunno
		public static List<float[]> CSRadiusList = new List<float[]>(); // Mystery too.
		public static List<string[]> CSColorList = new List<string[]>(); // List of colors, I guess. Probably for networks.
		public static List<string[]> CSLabelList = new List<string[]>(); // And of labels.
		
		public static List<List<Vector3>> FieldLineList= null;
		//public static ArrayList FieldLineDist= null;// Field lines distance arrays // Apparently not used anywhere
		
		//public static ArrayList CaSplineList=new ArrayList();

		/// <summary>
		/// The bonds between carbon alpha in the CA-Spline. Each element of this list is an int[2] where:
		/// int[0] is the index of the first CA,
		/// int[1] is the second one.
		/// </summary>
		public static List<int[]> bondCAList=new List<int[]>();

		/// <summary>
		/// Type of each carbon alpha in the CA-Spline. List of AtomModel.
		/// </summary>
		public static List<AtomModel> CaSplineTypeList = new List<AtomModel>();

		/// <summary>
		/// The chain of each carbon alpha in the CA-Spline.
		/// </summary>
		public static List<string> CaSplineChainList = new List<string>();

		/// <summary>
		/// Sometimes inside pdbs lists are not sorted, and residues mixed
		/// So I had to create this list to sort residues index by chain ID.
		/// </summary>
		public static List<int> sortedResIndexByList = new List<int>();

		/// <summary>
		/// Backup CaSplineChainList (chain of each carbon alpha in the CA-Spline).
		/// Used in ReSpline and BfactorRep
		/// </summary>
		public static List<string> backupCaSplineChainList = new List<string>();

		/// <summary>
		/// Bfactor of each atom.
		/// </summary>
		public static List<float> BFactorList = new List<float>();
		
		/// <summary>
		/// The atoms per ellipsoids per residue for HiRERNA rendering
		/// Key is the residue id
		/// Value is an array of atom ids (3 in any case) parameterizing the ellipsoid
		/// </summary>
		public static Dictionary<int, int[]> atomsForEllipsoidsPerResidue = new Dictionary<int, int[]>();
		
		public static Dictionary<int, int> atomsForEllipsoidsOrientationPerResidue = new Dictionary<int, int>();
		
		/// <summary>
		/// The index (in tables) of the base extremity.
		/// </summary>
		public static List<int> baseIdx = new List<int>();
		
		/// <summary>
		/// RNA Scale parameters
		/// </summary>
		public static List<float> scale_RNA = new List<float>();
		
		/// <summary>
		/// Contiguous (really?) list of ellipsoids
		/// </summary>
		public static List<GameObject> ellipsoids = new List<GameObject>();
		
		/// <summary>
		/// The ellipsoids per residue.
		/// </summary>
		public static Dictionary<int, GameObject> ellipsoidsPerResidue = new Dictionary<int, GameObject>();
		
		public static List<GameObject> bondsForReplacedAtoms = new List<GameObject>();

		public static List<Dictionary<string, Vector3>> residueDictionaries;
		public static List<Dictionary<string, Vector3>> residueDictionariesSugar;

		/// <summary>
		/// List of informations about each helix (extract from the pdb)
		/// float[0] is the first residue of each helix
		/// float[1] is the last residue of each helix
		/// float[2] the length of each helix
		/// float[3] the class of each helix
		/// </summary>
		public static List<float[]> ssHelixList    = new List<float[]> ();

		/// <summary>
		/// First and last residue of each strand (extract from the pdb)
		/// float[0] is the first residue of each strand
		/// float[1] is the last residue of each strand
		/// </summary>
		public static List<float[]> ssStrandList   = new List<float[]> ();

		/// <summary>
		/// The helix chain list (extract from the pdb).
		/// </summary>
		public static List<string> helixChainList  = new List<string>() ;

		/// <summary>
		/// The strand chain list (extract from the pdb).
		/// </summary>
		public static List<string> strandChainList = new List<string> ();
		
		public static Vector3 target=new Vector3(0f,0f,0f);//
		public static Vector3 cameraLocation=new Vector3(10f,10f,10f);//
		
		/// <summary>
		/// The offset for the molecule. The original barycenter of the molecule + this = (0,0,0). Vector3.
		/// Also used for density grids.
		/// </summary>
		public static Vector3 Offset=new Vector3(0f,0f,0f);
		
		/// <summary>
		/// The barycenter of the molecule. Vector3.
		/// </summary>
		public static Vector3 Center=new Vector3(0f,0f,0f);
		
		/// <summary>
		/// The "smallest" corner of the bounding box that encloses the molecule.
		/// </summary>
		public static Vector3 MinValue= new Vector3(0f,0f,0f);
		
		/// <summary>
		/// The "biggest" corner of the bounding box that encloses the molecule.
		/// </summary>
		public static Vector3 MaxValue= new Vector3(0f,0f,0f);

		// Luiz:
		// NEW PASTEL COLOR THEME
		private static Dictionary<string, Color> sElementsColors = new Dictionary<string, Color> {
			{"O", new Color(0.827f,0.294f,0.333f,1f)},
			{"C", new Color(0.282f,0.6f,0.498f,1f)},
			{"N", new Color(0.443f,0.662f,0.882f,1f)},
			{"H", Color.white},
			{"S", new Color(1f,0.839f,0.325f,1f)},
			{"P", new Color(0.960f,0.521f,0.313f,1f)},
			{"B", new Color(253f/255f, 181f/255f, 182f/255f)},
			{"F", new Color(208f/255f, 233f/255f, 80f/255f)},
			{"HE", new Color(218f/255f, 255f/255f, 255f/255f)},
			{"LI", new Color(203f/255f, 132f/255f, 252f/255f)},
			{"BE", new Color(195f/255f, 253f/255f, 52f/255f)},
			{"NE", new Color(181f/255f, 227f/255f, 244f/255f)},
			{"NA", new Color(170f/255f, 98f/255f, 239f/255f)},
			{"MG", new Color(142f/255f, 253f/255f, 49f/255f)},
			{"AL", new Color(189f/255f, 165f/255f, 165f/255f)},
			{"SI", new Color(239f/255f, 200f/255f, 162f/255f)},
			{"CL", new Color(51f/255f, 538f/255f, 55f/255f)},
			{"AR", new Color(131f/255f, 209f/255f, 223f/255f)},
			{"K", new Color(142f/255f, 71f/255f, 209f/255f)},
			{"CA", new Color(74f/255f, 253f/255f, 47f/255f)},
			{"SC", new Color(230f/255f, 230f/255f, 230f/255f)},
			{"TI", new Color(191f/255f, 194f/255f, 199f/255f)},
			{"V", new Color(166f/255f, 166f/255f, 171f/255f)},
			{"CR", new Color(139f/255f, 154f/255f, 197f/255f)},
			{"MN", new Color(156f/255f, 124f/255f, 197f/255f)},
			{"FE", new Color(222f/255f, 102f/255f, 59f/255f)},
			{"CO", new Color(238f/255f, 145f/255f, 161f/255f)},
			{"NI", new Color(86f/255f, 206f/255f, 207f/255f)},
			{"CU", new Color(198f/255f, 128f/255f, 59f/255f)},
			{"ZN", new Color(125f/255f, 129f/255f, 124f/255f)},
			{"GA", new Color(193f/255f, 142f/255f, 144f/255f)},
			{"GE", new Color(103f/255f, 143f/255f, 143f/255f)},
			{"AS", new Color(188f/255f, 131f/255f, 225f/255f)},
			{"SE", new Color(153f/255f, 160f/255f, 40f/255f)},
			{"BR", new Color(164f/255f, 42f/255f, 45f/255f)},
			{"KR", new Color(96f/255f, 184f/255f, 208f/255f)},
			{"RB", new Color(111f/255f, 53f/255f, 173f/255f)},
			{"SR", new Color(41f/255f, 253f/255f, 47f/255f)},
			{"Y", new Color(152f/255f, 255f/255f, 254f/255f)},
			{"ZR", new Color(151f/255f, 224f/255f, 223f/255f)},
			{"NB", new Color(118f/255f, 194f/255f, 200f/255f)},
			{"MO", new Color(88f/255f, 181f/255f, 180f/255f)},
			{"TC", new Color(64f/255f, 158f/255f, 157f/255f)},
			{"RU", new Color(43f/255f, 143f/255f, 142f/255f)},
			{"RH", new Color(23f/255f, 125f/255f, 139f/255f)},
			{"PD", new Color(13f/255f, 103f/255f, 129f/255f)},
			{"AG", new Color(192f/255f, 192f/255f, 192f/255f)},
			{"CD", new Color(254f/255f, 216f/255f, 148f/255f)},
			{"IN", new Color(165f/255f, 117f/255f, 116f/255f)},
			{"SN", new Color(103f/255f, 128f/255f, 128f/255f)},
			{"SB", new Color(157f/255f, 102f/255f, 179f/255f)},
			{"TE", new Color(210f/255f, 122f/255f, 30f/255f)},
			{"I", new Color(146f/255f, 19f/255f, 146f/255f)},
			{"XE", new Color(71f/255f, 178f/255f, 175f/255f)},
			{"CS", new Color(86f/255f, 31f/255f, 141f/255f)},
			{"BA", new Color(30f/255f, 199f/255f, 34f/255f)},
			{"LA", new Color(116f/255f, 213f/255f, 253f/255f)},
			{"CE", new Color(255f/255f, 254f/255f, 202f/255f)},
			{"PR", new Color(218f/255f, 254f/255f, 201f/255f)},
			{"ND", new Color(201f/255f, 254f/255f, 201f/255f)},
			{"PM", new Color(166f/255f, 254f/255f, 201f/255f)},
			{"SM", new Color(147f/255f, 254f/255f, 200f/255f)},
			{"EU", new Color(105f/255f, 254f/255f, 200f/255f)},
			{"GD", new Color(81f/255f, 254f/255f, 200f/255f)},
			{"TB", new Color(65f/255f, 254f/255f, 200f/255f)},
			{"DY", new Color(55f/255f, 254f/255f, 200f/255f)},
			{"HO", new Color(42f/255f, 253f/255f, 159f/255f)},
			{"ER", new Color(37f/255f, 228f/255f, 122f/255f)},
			{"TM", new Color(33f/255f, 210f/255f, 89f/255f)},
			{"YB", new Color(28f/255f, 189f/255f, 25f/255f)},
			{"LU", new Color(24f/255f, 169f/255f, 47f/255f)},
			{"HF", new Color(84f/255f, 195f/255f, 252f/255f)},
			{"TA", new Color(82f/255f, 168f/255f, 252f/255f)},
			{"W", new Color(43f/255f, 149f/255f, 211f/255f)},
			{"RE", new Color(44f/255f, 126f/255f, 169f/255f)},
			{"OS", new Color(48f/255f, 103f/255f, 149f/255f)},
			{"IR", new Color(27f/255f, 85f/255f, 133f/255f)},
			{"PT", new Color(208f/255f, 208f/255f, 223f/255f)},
			{"AU", new Color(254f/255f, 208f/255f, 60f/255f)},
			{"HG", new Color(184f/255f, 184f/255f, 207f/255f)},
			{"TL", new Color(165f/255f, 84f/255f, 79f/255f)},
			{"PB", new Color(87f/255f, 89f/255f, 97f/255f)},
			{"BI", new Color(157f/255f, 83f/255f, 179f/255f)},
			{"PO", new Color(169f/255f, 92f/255f, 21f/255f)},
			{"AT", new Color(116f/255f, 79f/255f, 70f/255f)},
			{"RN", new Color(69f/255f, 130f/255f, 149f/255f)},
			{"FR", new Color(65f/255f, 8f/255f, 100f/255f)},
			{"RA", new Color(15f/255f, 124f/255f, 17f/255f)},
			{"AC", new Color(115f/255f, 173f/255f, 247f/255f)},
			{"TH", new Color(32f/255f, 187f/255f, 252f/255f)},
			{"PA", new Color(28f/255f, 163f/255f, 252f/255f)},
			{"U", new Color(25f/255f, 146f/255f, 251f/255f)},
			{"NP", new Color(22f/255f, 131f/255f, 251f/255f)},
			{"PU", new Color(119f/255f, 112f/255f, 251f/255f)},
			{"AM", new Color(85f/255f, 97f/255f, 238f/255f)},
			{"CM", new Color(120f/255f, 97f/255f, 224f/255f)},
			{"BK", new Color(137f/255f, 85f/255f, 224f/255f)},
			{"CF", new Color(160f/255f, 63f/255f, 209f/255f)},
			{"ES", new Color(137f/255f, 45f/255f, 209f/255f)},
			{"FM", new Color(177f/255f, 43f/255f, 184f/255f)},
			{"MD", new Color(177f/255f, 29f/255f, 164f/255f)},
			{"NO", new Color(187f/255f, 26f/255f, 134f/255f)},
			{"LR", new Color(197f/255f, 15f/255f, 102f/255f)},
			{"RF", new Color(202f/255f, 13f/255f, 90f/255f)},
			{"DB", new Color(207f/255f, 13f/255f, 81f/255f)},
			{"SG", new Color(215f/255f, 12f/255f, 72f/255f)},
			{"BH", new Color(221f/255f, 12f/255f, 61f/255f)},
			{"HS", new Color(227f/255f, 12f/255f, 53f/255f)},
			{"MT", new Color(232f/255f, 12f/255f, 47f/255f)}
		};
		public static Color oxygenColor { get { return GetAtomColor("O"); } }
		public static Color carbonColor { get { return GetAtomColor("C"); } }
		public static Color nitrogenColor { get { return GetAtomColor("N"); } }
		public static Color hydrogenColor { get { return GetAtomColor("H"); } }
		public static Color sulphurColor { get { return GetAtomColor("S"); } }
		public static Color phosphorusColor { get { return GetAtomColor("P"); } }
		public static Color boronColor { get { return GetAtomColor("B"); } }
		public static Color fluorineColor { get { return GetAtomColor("F"); } }
		public static readonly Color unknownColor = Color.magenta;
//		public static ColorObject selectionColor = new ColorObject(Color.red);
//		public static ColorObject residueColor = new ColorObject(Color.white);		
		public static Color GetAtomColor(string atomType) {
			var atomKey = FindAtomNameKeyInDictionary(atomType, sElementsColors);

			return atomKey != null ? sElementsColors[atomKey] : unknownColor;
		}

		public static string FindAtomNameKeyInDictionary(string atomType, IDictionary dictionary) {
			atomType = atomType.ToUpper();

			for (int idx = atomType.Length; idx > 0; --idx) {
				string key = atomType.Substring(0, idx);
				if (dictionary.Contains(key)) {
					return key;
				}
			}

			return null;
		}			

		private static Dictionary<string, float> sElementsRadius = new Dictionary<string, float> {
			{"O", 3.04f},
			{"C", 3.4f},
			{"N", 3.1f},
			{"H", 2.4f},
			{"S", 4.54f},
			{"P", 3.6f},
		};
		public const float unknowRadius = 2f;
		public static float GetAtomRadius(string atomType) {
			atomType = atomType.ToUpper();
			return sElementsRadius.ContainsKey(atomType) ? sElementsRadius[atomType] : unknowRadius;
		}
	
		public static string oxygenNumber="0";
		public static string carbonNumber="0";
		public static string nitrogenNumber="0";	
		public static string hydrogenNumber="0";	
		public static string sulphurNumber="0";
		//public static string lodineNumber="0";
		//public static string chlorineNumber="0";		
		public static string phosphorusNumber="0";
		public static string unknownNumber="0";
		
		// public static GameObject [] boxes;
/*
		public static GameObject[] Oes;		
		public static GameObject[] Ces;
		public static GameObject[] Nes;
		public static GameObject[] Hes;		
		public static GameObject[] Ses;
		//public static GameObject[] Les;
		//public static GameObject[] Cles;
		public static GameObject[] Pes;		
		public static GameObject[] NOes;
*/
		
		public static Dictionary<string, GameObject[]> atomsByChar = new Dictionary<string, GameObject[]>();
		public static ArrayList atoms = new ArrayList();
		
		public static GameObject[] clubs;
		
		//public static float []atomsScaleList={1.72f,1.6f,1.32f,2.08f,2.6f,1f};//c\n\o\s\p\n
		
		public static Vector3 vo=new Vector3(0.66f,0.66f,0.66f);		
		public static Vector3 vc=new Vector3(0.86f,0.86f,0.86f);
		public static Vector3 vn=new Vector3(0.80f,0.80f,0.80f);
		public static Vector3 vh=new Vector3(0.78f,0.78f,0.78f);		
		public static Vector3 vs=new Vector3(1.04f,1.04f,1.04f);
		//public static Vector3 vl=new Vector3(1.95f,1.95f,1.95f);
		//public static Vector3 vcl=new Vector3(0.91f,0.91f,0.91f);		
		public static Vector3 vp=new Vector3(1.30f,1.30f,1.30f);
		public static Vector3 vno=new Vector3(1f,1f,1f);
		
		public static	float oxygenScale=100f;
		public static 	float carbonScale=100f;
		public static 	float nitrogenScale=100f;
		public static 	float hydrogenScale=100f;		
		public static	float sulphurScale=100f;
		//public static	string lodineScale="100";
		//public static	string chlorineScale="100";
		public static	float phosphorusScale=100f;
		public static 	float unknownScale=100f;
		
		public static long atomsnumber=0;
		public static long bondsnumber=0;
		
		public static string FPS="";
		
		public static Particle[] p;
		
		public static Particle[] fieldlinep;

		public static string newtooltip;
		
		public static bool fieldLineFileExists=false;
		
		public static bool dxFileExists = false ; // true if a DX file was found

		public static bool surfaceFileExists=false;

		public static bool useHetatmForSurface = false;

		public static bool useSugarForSurface = false;


		public static bool networkLoaded = false; // set to true when a network is present
		
		public static Vector3[] vertices;
		
		public MoleculeModel() {}
	}
}
