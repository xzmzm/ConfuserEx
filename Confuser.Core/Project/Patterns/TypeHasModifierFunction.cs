using System;
using dnlib.DotNet;

namespace Confuser.Core.Project.Patterns {
	/// <summary>
	///     A function that indicate whether the item's class type has the given modifier(s).
	/// </summary>
	public class TypeHasModifierFunction : PatternFunction {
		internal const string FnName = "type-has-modifier";

		/// <inheritdoc />
		public override string Name {
			get { return FnName; }
		}

		/// <inheritdoc />
		public override int ArgumentCount {
			get { return 1; }
		}

		/// <inheritdoc />
		public override object Evaluate(IDnlibDef definition) {
			if (!(definition is IMemberDef memberDef) || memberDef.DeclaringType == null)
				return false;
			var type = memberDef.DeclaringType;
			string s = Arguments[0].Evaluate(definition).ToString();
			var modifiers = s.Split('|');
			foreach (var modifier in modifiers) {
				switch (modifier.Trim().ToLowerInvariant()) {
					case "public":
						if (type.IsPublic || type.IsNestedPublic)
							return true;
						break;
					case "private":
						if (type.IsNestedPrivate)
							return true;
						break;
					case "protected":
						if (type.IsNestedFamily)
							return true;
						break;
					case "internal":
						if (type.IsNestedAssembly)
							return true;
						break;
					case "protected internal":
						if (type.IsNestedFamilyOrAssembly)
							return true;
						break;
					case "private protected":
						if (type.IsNestedFamilyAndAssembly)
							return true;
						break;
					case "abstract":
						if (type.IsAbstract)
							return true;
						break;
					case "sealed":
						if (type.IsSealed)
							return true;
						break;
					case "static":
						if (type.IsAbstract && type.IsSealed)
							return true;
						break;
				}
			}
			return false;
		}
	}
}
