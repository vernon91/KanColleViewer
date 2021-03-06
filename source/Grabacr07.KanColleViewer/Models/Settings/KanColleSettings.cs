﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using MetroTrilithon.Serialization;

namespace Grabacr07.KanColleViewer.Models.Settings
{
	/// <summary>
	/// 艦これの動作に関連する設定を表す静的プロパティを公開します。
	/// </summary>
	public class KanColleSettings : IKanColleClientSettings
	{
		/// <summary>
		/// 建造中の艦の名前を表示するかどうかを示す設定値を取得します。
		/// </summary>
		public static SerializableProperty<bool> CanDisplayBuildingShipName { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Roaming, false);

		/// <summary>
		/// 索敵計算に使用するロジックを識別する文字列の設定値を取得します。
		/// </summary>
		public static SerializableProperty<string> ViewRangeCalcType { get; }
			= new SerializableProperty<string>(GetKey(), Providers.Roaming, new ViewRangeType1().Id);

		/// <summary>
		/// 建造完了時に通知するかどうかを示す設定値を取得します。
		/// </summary>
		public static SerializableProperty<bool> NotifyBuildingCompleted { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Roaming, false);

		/// <summary>
		/// 遠征帰投時に通知するかどうかを示す設定値を取得します。
		/// </summary>
		public static SerializableProperty<bool> NotifyExpeditionReturned { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Roaming, false);

		/// <summary>
		/// 入渠完了時に通知するかどうかを示す設定値を取得します。
		/// </summary>
		public static SerializableProperty<bool> NotifyRepairingCompleted { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Roaming, false);

		/// <summary>
		/// 入渠完了と遠征帰還の通知における、通知短縮時間 (秒) を表す設定値を取得します。
		/// </summary>
		public static SerializableProperty<int> NotificationShorteningTime { get; }
			= new SerializableProperty<int>(GetKey(), Providers.Roaming, 40);

		/// <summary>
		/// 艦隊のコンディション値が出撃可能となったときに通知するかどうかを示す設定値を取得します。
		/// </summary>
		public static SerializableProperty<bool> NotifyFleetRejuvenated { get; }
			= new SerializableProperty<bool>(GetKey(), Providers.Roaming, false);

		/// <summary>
		/// 艦隊が再出撃可能と判断する基準となるコンディション値を表す設定値を取得します。
		/// </summary>
		public static SerializableProperty<int> ReSortieCondition { get; }
			= new SerializableProperty<int>(GetKey(), Providers.Roaming, 49);

		/// <summary>
		/// 画面に表示する資材 (1 つめ) を表す設定値を取得します。
		/// </summary>
		public static SerializableProperty<string> DisplayMaterial1 { get; }
			= new SerializableProperty<string>(GetKey(), Providers.Roaming, nameof(Materials.InstantRepairMaterials));

		/// <summary>
		/// 画面に表示する資材 (2 つめ) を表す設定値を取得します。
		/// </summary>
		public static SerializableProperty<string> DisplayMaterial2 { get; }
			= new SerializableProperty<string>(GetKey(), Providers.Roaming, nameof(Materials.InstantBuildMaterials));


		#region instance members

		public event PropertyChangedEventHandler PropertyChanged;

		public KanColleSettings()
		{
			NotificationShorteningTime.Subscribe(_ => this.RaisePropertyChanged(nameof(NotificationShorteningTime)));
			ReSortieCondition.Subscribe(_ => this.RaisePropertyChanged(nameof(ReSortieCondition)));
			ViewRangeCalcType.Subscribe(_ => this.RaisePropertyChanged(nameof(ViewRangeCalcType)));
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region IKanColleClientSettings members

		int IKanColleClientSettings.NotificationShorteningTime => NotificationShorteningTime.Value;

		int IKanColleClientSettings.ReSortieCondition => ReSortieCondition.Value;

		string IKanColleClientSettings.ViewRangeCalcType => ViewRangeCalcType.Value;

		#endregion


		private static string GetKey([CallerMemberName] string propertyName = "")
		{
			return nameof(KanColleSettings) + "." + propertyName;
		}
	}
}
