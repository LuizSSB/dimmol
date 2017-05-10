# Dimmol
----------

Dimmol (acronym for Distributed Immersive Multi-platform Molecular visualization) is a scientific visualization application based on UnityMol (https://github.com/bam93/UnityMol-Releases) that uses the Unity Cluster Package (https://sourceforge.net/projects/unityclusterpackage) to enable distributed and immersive visualization of molecular structures across multiple device types.

Currently, UCP has been integrated in UnityMol version 0.9.3 in order to allow the molecular structures to be displayed in graphic clusters using the master-slave rendering architecture. Several modifications have been made to UnityMol's source code so that it is possible to synchronize a molecular structure initially loaded on the master node, as well as changes applied to its view and representation, with the slave nodes. This synchronization was done using Unity's remote process call (RPC) capabilities in three main ways:

- Modifying key methods (static and in singletons) so that if they are invoked on the master node, they call themselves by RPC on the slave nodes;
- Changing key variables and properties (static and in singletons) so that in case their values are changed on the master node, special methods on the slave nodes are invoked by RPC to assign the new values to those variables / properties; and
- Creation and invocation by RPC of specific methods to synchronize certain changes, when the previous two cases are not applicable.

This approach involved identifying and changing methods, variables, properties, and key code fragments. However, it allowed the synchronization of data through nodes to be implemented with minimal interference to the actual logic of the application.

This feature was the basis upon which all other following features were implemented.

### Multi-Platform Support

Because UnityMol (and, consequently, Dimmol) is developed with Unity, which offers great multi-platform support, it is possible to compile it to a variety of platforms with little to no effort. Dimmol, specifically, was tested on PCs, Macs, iOS and Android.

In general, builds of Dimmol to all platforms offer the same features and visual style, as they share the same code base. Visualizations can be distributed across devices of different platforms thanks to UCP, as it considers a graphic cluster any group of devices connected through the internet and executing the same application. In addition, all devices of all platforms may act as master or slave node.

Android and iOS versions, however, have one big difference: Google VR support, which allows for stereoscopic and immersive visualization using Google Cardboard or another similar HMD. This feature can be enabled / disabled individually on each device. When enabled, two virtual cameras are created side by side in the VE (their images displayed side by side in the device) what, together with the HMD, causes the stereoscopy effect.

Also, for the correct operation of GoogleVR, the user's head movement must be tracked. Because of this, integration with GoogleVR's software development kit (SDK) added to Dimmol the capability of manipulating the molecular structure view with 6 degrees-of-freedom (6DOF) using the gyroscope available in mobile devices, even when Google VR's stereoscopic visualization is not enabled.

### UcpGui Scene

UCP makes use of a configuration file that describes how the node should behave (its type, view parameters, master node IP address, etc.). In order to make it easy to start an instance of Dimmol without having to manually edit this file, an initial scene named UcpGui (acronym for Unity Cluster Package Graphical User Interface) was created.

It features a user interface mapping each configuration option of UCP to a visual control that can be edited by the device's user. It allows the user to properly start Dimmol using the values set in the controls and also to save them so that, later, changes made to the configuration are reloaded.

### Master-Host-Slave Rendering Architecture

UCP has also been modified to support a rendering architecture called in this paper as master-host-slave.

In this architecture, the master node responsibilities are divided into two nodes, the host and the master. The host hosts the application and all other nodes connect to it, however, it has no other responsibility. The master node now connects to the host in the same way as a slave node, but it has the power to coordinate the application; all actions taken on the master are passed to the host, which then redistributes them to the other nodes. Lastly, the slave nodes work in the same way as before. 

With the master-host-slave architecture, application coordination can be unlinked from hosting, allowing less computationally powerful devices, such as smartphones and tablets, to coordinate the execution without the weight of serving it to all other nodes.

### Free Visualization on Slave Devices

By default, when Dimmol is displaying a molecular structure, the master node forces its manipulation and representation of the view to the other nodes. In some cases, however, it is desired for slave nodes (or a subset of them) to have the power to manipulate their own view (bt spinning, moving and / or zooming it), without affecting the other nodes.

Therefore, this capability has been added to Dimmol. The master node is responsible for selecting the molecular structure and its representation, however, the slave nodes may have the power to manipulate their own view of the structure individually as its users desire.

While this feature may be problematic for visualization on CAVE-like systems, it can be used in cases where the nodes of the application are devices with independent displays (e.g. set of smartphones, each used by a different person). Because of this, the activation of this feature is done individually in each device.

As for the application controls, they are displayed and enabled only on the master node, in order to prevent other types of nodes from interfering with the execution of the application or leaving the correct synchronization state.

### Trajectory files

UnityMol version 0.9.3 supports loading molecular trajectories (sequences of states of molecular systems) using the MDDriver (https://sourceforge.net/projects/mddriver/) library, however, it is not possible to load trajectory files already on the disk. With this in mind, it was implemented in Dimmol the capability of loading trajectories files stored on disk or available at some web address.

Trajectories may be described using the XMOL format (which can be understood as a sequence of XYZ descriptors) or contain the output of a geometry optimization operation calculated by GAMESS (http://www.msg.chem.iastate.edu/index.html).

Only one state of the trajectory is displayed at a time. It is possible to control which state is currently displayed or to animate the transition between states, so that it is possible to analyze the evolution of the system in a continuous way.

On the master node, after a trajectory file is selected or loaded from the web, Dimmol reads the entire file, identifying and extracting each state of the trajectory. This data is then synchronized with the other nodes. Depending on the number of atoms in the system, the number of states and the connection between the nodes, synchronization may take a while (in the users' perception); in spite of this negative, this approach later allows the transition between states to be more efficient, since, for that, only the index of the new state is synchronized, instead of all the information about the atoms' positions, leaving the raw work of loading and rendering the state for each node.

In any of the states of the trajectory, as well as during the evolution animation, total control is given to the user so that he/she can change the parameters of representation, point of view and other details, as he / she would with any other type of molecular structure loaded in UnityMol.

In the case of trajectories loaded from a GAMESS output, the file will also contain energy information for each state, so when loading these trajectories, Dimmol can display a vertical energy meter in which a pointer is placed in relation to the energy of the current state.

### Demonstration
The following video is a demonstration of Dimmol being used at the Laboratory of Interfaces and Visualization (LIV), part of the Computing Department (DCo) of the School of Sciences (FC) at São Paulo State University (UNESP), Bauru, Brazil. 
[![Project Prototype - UnityMol + Unity Cluster Package + Trajectory](http://img.youtube.com/vi/dxPBcE5LjtY/0.jpg)](http://www.youtube.com/watch?v=dxPBcE5LjtY "Project Prototype - UnityMol + Unity Cluster Package + Trajectory")


### Acknowledgements
The copyright for UnityMol is held by the Centre National de la Recherche Scientifique (CNRS), France. UnityMol is developed by FvNano/LBT Team, and Marc Baaden, Ph.D. The source-code for version 0.9.3 of the software was downloaded from the official public repository.

### TODO
- Make the UcpGui scene more usable. 