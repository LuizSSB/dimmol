using UnityEngine;
using System.Collections;
using UnityClusterPackage;
using System;

using Converter = System.Convert;

namespace Config {
	[System.Flags]
	public enum NodePermission {
		Master = 1,
		Server = 1 << 1,
		Client = 1 << 2,
		Slave = 1 << 3,
		MenuControl = 1 << 4,
		CameraControl = 1 << 5,
		Energy = 1 << 6
	}

	public static class NodeExtensions {
		// Luiz: The most adequate way to deal with this would probably be having a dependency injection scheme,
		// however for the time being this will do.
		public static bool HasPermission(this Node node, NodePermission permission)
		{
			if (permission.IsFlagSet(NodePermission.Slave))
			if (Node.CurrentNode.NodeType != Node.Type.slave)
				return false;

			if (permission.IsFlagSet(NodePermission.Master))
			if (Node.CurrentNode.NodeType != Node.Type.masterHost)
				return false;

			if (permission.IsFlagSet(NodePermission.Server))
			if (Node.CurrentNode.NodeType != Node.Type.host)
				return false;

			if (permission.IsFlagSet(NodePermission.Client))
			if (Node.CurrentNode.NodeType != Node.Type.master)
				return false;

			if (permission.IsFlagSet(NodePermission.CameraControl))
			if (Node.CurrentNode.NodeType == Node.Type.host
				|| (Node.CurrentNode.NodeType == Node.Type.slave && !SlaveConfig.CurrentConfig.CameraControl))
				return false;

			if (permission.IsFlagSet(NodePermission.MenuControl))
			if (Node.CurrentNode.NodeType != Node.Type.masterHost && Node.CurrentNode.NodeType != Node.Type.master)
				return false;

			if (permission.IsFlagSet(NodePermission.Energy))
			if (Node.CurrentNode.NodeType != Node.Type.masterHost && Node.CurrentNode.NodeType != Node.Type.master
				&& !SlaveConfig.CurrentConfig.ShowEnergy)
				return false;

			return true;
		}

		// Luiz: could/should be a generic method, but opting to hardcode the type in order to make it faster.
		private static bool IsFlagSet(this NodePermission permission, NodePermission flag)
		{
			return (permission & flag) != 0;
		}
	}
}