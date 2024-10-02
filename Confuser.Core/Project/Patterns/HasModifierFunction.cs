using System;
using dnlib.DotNet;

namespace Confuser.Core.Project.Patterns {
	/// <summary>
	///     A function that indicate whether the item has the given modifier(s).
	/// </summary>
	public class HasModifierFunction : PatternFunction {
		internal const string FnName = "has-modifier";

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
			string s = Arguments[0].Evaluate(definition).ToString();
			var modifiers = s.Split('|');
			foreach (var a in modifiers) {
				switch (a) {
					case "public":
						if (definition is MethodDef methodPublic && methodPublic.IsPublic)
							return true;
						if (definition is FieldDef fieldPublic && fieldPublic.IsPublic)
							return true;
						if (definition is PropertyDef propertyPublic && propertyPublic.IsPublic())
							return true;
						if (definition is EventDef eventPublic && eventPublic.IsPublic())
							return true;
						if (definition is TypeDef typePublic && typePublic.IsPublic)
							return true;
						break;
					case "private":
						if (definition is MethodDef methodPrivate && methodPrivate.IsPrivate)
							return true;
						if (definition is FieldDef fieldPrivate && fieldPrivate.IsPrivate)
							return true;
						if (definition is PropertyDef propertyPrivate && propertyPrivate.GetMethod != null && propertyPrivate.GetMethod.IsPrivate)
							return true;
						if (definition is EventDef eventPrivate && eventPrivate.AddMethod != null && eventPrivate.AddMethod.IsPrivate)
							return true;
						if (definition is TypeDef typePrivate && typePrivate.IsNestedPrivate)
							return true;
						break;
					case "protected":
						if (definition is MethodDef methodProtected && methodProtected.IsFamily)
							return true;
						if (definition is FieldDef fieldProtected && fieldProtected.IsFamily)
							return true;
						if (definition is PropertyDef propertyProtected && propertyProtected.IsFamily())
							return true;
						if (definition is EventDef eventProtected && eventProtected.IsFamily())
							return true;
						if (definition is TypeDef typeProtected && typeProtected.IsNestedFamily)
							return true;
						break;
					case "internal":
						if (definition is MethodDef methodInternal && methodInternal.IsAssembly)
							return true;
						if (definition is FieldDef fieldInternal && fieldInternal.IsAssembly)
							return true;
						if (definition is PropertyDef propertyInternal && propertyInternal.GetMethod != null && propertyInternal.GetMethod.IsAssembly)
							return true;
						if (definition is EventDef eventInternal && eventInternal.AddMethod != null && eventInternal.AddMethod.IsAssembly)
							return true;
						if (definition is TypeDef typeInternal && typeInternal.IsNestedAssembly)
							return true;
						break;
					case "protected internal":
						if (definition is MethodDef methodProtectedInternal && methodProtectedInternal.IsFamilyOrAssembly)
							return true;
						if (definition is FieldDef fieldProtectedInternal && fieldProtectedInternal.IsFamilyOrAssembly)
							return true;
						if (definition is PropertyDef propertyProtectedInternal && propertyProtectedInternal.IsFamilyOrAssembly())
							return true;
						if (definition is EventDef eventProtectedInternal && eventProtectedInternal.IsFamilyOrAssembly())
							return true;
						if (definition is TypeDef typeProtectedInternal && typeProtectedInternal.IsNestedFamilyOrAssembly)
							return true;
						break;
					case "private protected":
						if (definition is MethodDef methodPrivateProtected && methodPrivateProtected.IsFamilyAndAssembly)
							return true;
						if (definition is FieldDef fieldPrivateProtected && fieldPrivateProtected.IsFamilyAndAssembly)
							return true;
						if (definition is PropertyDef propertyPrivateProtected && propertyPrivateProtected.GetMethod != null && propertyPrivateProtected.GetMethod.IsFamilyAndAssembly)
							return true;
						if (definition is EventDef eventPrivateProtected && eventPrivateProtected.AddMethod != null && eventPrivateProtected.AddMethod.IsFamilyAndAssembly)
							return true;
						if (definition is TypeDef typePrivateProtected && typePrivateProtected.IsNestedFamilyAndAssembly)
							return true;
						break;
					case "static":
						if (definition is MethodDef methodStatic && methodStatic.IsStatic)
							return true;
						if (definition is FieldDef fieldStatic && fieldStatic.IsStatic)
							return true;
						if (definition is PropertyDef propertyStatic && propertyStatic.IsStatic())
							return true;
						if (definition is EventDef eventStatic && eventStatic.IsStatic())
							return true;
						if (definition is TypeDef typeStatic && typeStatic.IsAbstract && typeStatic.IsSealed)
							return true;
						break;
					case "virtual":
						if (definition is MethodDef methodVirtual && methodVirtual.IsVirtual)
							return true;
						if (definition is PropertyDef propertyVirtual && propertyVirtual.GetMethod != null && propertyVirtual.GetMethod.IsVirtual)
							return true;
						if (definition is EventDef eventVirtual && eventVirtual.AddMethod != null && eventVirtual.AddMethod.IsVirtual)
							return true;
						break;
					case "abstract":
						if (definition is MethodDef methodAbstract && methodAbstract.IsAbstract)
							return true;
						if (definition is PropertyDef propertyAbstract && propertyAbstract.IsAbstract())
							return true;
						if (definition is EventDef eventAbstract && eventAbstract.IsAbstract())
							return true;
						if (definition is TypeDef typeAbstract && typeAbstract.IsAbstract)
							return true;
						break;
					case "sealed":
						if (definition is MethodDef methodSealed && methodSealed.IsFinal)
							return true;
						if (definition is TypeDef typeSealed && typeSealed.IsSealed)
							return true;
						break;

					case "override":
						if (definition is MethodDef methodOverride && methodOverride.IsVirtual && methodOverride.IsReuseSlot)
							return true;
						if (definition is PropertyDef propertyOverride && propertyOverride.GetMethod != null && propertyOverride.GetMethod.IsVirtual && propertyOverride.GetMethod.IsReuseSlot)
							return true;
						if (definition is EventDef eventOverride && eventOverride.AddMethod != null && eventOverride.AddMethod.IsVirtual && eventOverride.AddMethod.IsReuseSlot)
							return true;
						break;
				}
			}
			return false;
		}
	}
}
