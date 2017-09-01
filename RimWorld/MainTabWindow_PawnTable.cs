using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorld
{
	public abstract class MainTabWindow_PawnTable : MainTabWindow
	{
		private PawnTable table;

		protected virtual float ExtraBottomSpace
		{
			get
			{
				return 53f;
			}
		}

		protected virtual float ExtraTopSpace
		{
			get
			{
				return 0f;
			}
		}

		protected abstract PawnTableDef PawnTableDef
		{
			get;
		}

		protected virtual IEnumerable<Pawn> Pawns
		{
			get
			{
				return Find.VisibleMap.mapPawns.FreeColonists;
			}
		}

		protected override float Margin
		{
			get
			{
				return 6f;
			}
		}

		public override Vector2 RequestedTabSize
		{
			get
			{
				if (this.table == null)
				{
					return Vector2.zero;
				}
				return new Vector2(this.table.Size.x + this.Margin * 2f, this.table.Size.y + this.ExtraBottomSpace + this.ExtraTopSpace + this.Margin * 2f);
			}
		}

		public override void PostOpen()
		{
			if (this.table == null)
			{
				this.table = this.CreateTable();
			}
			this.SetDirty();
		}

		public override void DoWindowContents(Rect rect)
		{
			base.DoWindowContents(rect);
			this.table.PawnTableOnGUI(new Vector2(rect.x, rect.y + this.ExtraTopSpace));
		}

		public void Notify_PawnsChanged()
		{
			this.SetDirty();
		}

		public override void Notify_ResolutionChanged()
		{
			this.table = this.CreateTable();
			base.Notify_ResolutionChanged();
		}

		private PawnTable CreateTable()
		{
			return new PawnTable(this.PawnTableDef, () => this.Pawns, 998, UI.screenWidth - (int)(this.Margin * 2f), 0, (int)((float)(UI.screenHeight - 35) - this.ExtraBottomSpace - this.ExtraTopSpace - this.Margin * 2f));
		}

		protected void SetDirty()
		{
			this.table.SetDirty();
			this.SetInitialSizeAndPosition();
		}
	}
}