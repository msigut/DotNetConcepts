using System.Diagnostics;
using CsvHelper.Configuration;

namespace CsvImporter
{
	[DebuggerDisplay("jmeno = {jmeno}")]
	public class ImportKusovka
	{
#pragma warning disable IDE1006 // Naming Styles

		public int id { get; set; }
		public string produkt_id { get; set; }
		public string jmeno { get; set; }
		public string jmeno_karty { get; set; }
		public string poznamka { get; set; }
		public string verze { get; set; }
		public string poznamka_stav { get; set; }
		public string cena { get; set; }
		public string obrazek { get; set; }
		public string popis { get; set; }
		public string pow { get; set; }
		public string thg { get; set; }
		public string rarita { get; set; }
		public string typ { get; set; }
		public string edice { get; set; }
		public string stav { get; set; }
		public string jazyk { get; set; }
		public string foil { get; set; }
		public string castingcost { get; set; }
		public string barva { get; set; }
		public string extended { get; set; }
		public string ve_vykupu { get; set; }
		public string skladem_brno { get; set; }
		public string skladem_praha { get; set; }
		public string rezervace { get; set; }
		public string cena_nakup { get; set; }
		public string pocetcc { get; set; }
		public string kusupozadujeme { get; set; }
		public string zobrazovat_max { get; set; }
		public string carovy_kod { get; set; }
		public string timemark { get; set; }
		public int sklad { get; set; }
		public int v_boosteru { get; set; }
		public string problemova { get; set; }
		public string reserved_list { get; set; }
		public string spolecna { get; set; }
		public string barva_razeni { get; set; }
		public string mkm_id { get; set; }
		public int typ_precenovani { get; set; }
		public string smazat { get; set; }
		public string pultovka { get; set; }
		public string katalog { get; set; }
		public string vykup_mailem { get; set; }

#pragma warning restore IDE1006 // Naming Styles

		public string ToLog()
		{
			return $"{jmeno.Trim()}, {jazyk.Trim()}-{stav.Trim()}";
		}
	}

	/// <summary>
	/// zadani pro Automatic class generator for CsvHelper
	/// https://blog.tedd.no/2017/09/26/automatic-class-generator-for-csvhelper/
	/// </summary>
	/// <remarks>
	/// id,produkt_id,jmeno,jmeno_karty,poznamka,verze,poznamka_stav,cena,obrazek,popis,pow,thg,rarita,typ,edice,stav,jazyk,foil,castingcost,barva,extended,ve_vykupu,skladem_brno,skladem_praha,rezervace,cena_nakup,pocetcc,kusupozadujeme,zobrazovat_max,carovy_kod,timemark,sklad,v_boosteru,problemova,reserved_list,spolecna,barva_razeni,mkm_id,typ_precenovani,smazat,pultovka,katalog,vykup_mailem
	/// </remarks>
	public class KusovkaMapper : ClassMap<ImportKusovka>
	{
		public KusovkaMapper()
		{
			var i = 0;
			Map(m => m.id).Index(i++);
			Map(m => m.produkt_id).Index(i++);
			Map(m => m.jmeno).Index(i++);
			Map(m => m.jmeno_karty).Index(i++);
			Map(m => m.poznamka).Index(i++);
			Map(m => m.verze).Index(i++);
			Map(m => m.poznamka_stav).Index(i++);
			Map(m => m.cena).Index(i++);
			Map(m => m.obrazek).Index(i++);
			Map(m => m.popis).Index(i++);
			Map(m => m.pow).Index(i++);
			Map(m => m.thg).Index(i++);
			Map(m => m.rarita).Index(i++);
			Map(m => m.typ).Index(i++);
			Map(m => m.edice).Index(i++);
			Map(m => m.stav).Index(i++);
			Map(m => m.jazyk).Index(i++);
			Map(m => m.foil).Index(i++);
			Map(m => m.castingcost).Index(i++);
			Map(m => m.barva).Index(i++);
			Map(m => m.extended).Index(i++);
			Map(m => m.ve_vykupu).Index(i++);
			Map(m => m.skladem_brno).Index(i++);
			Map(m => m.skladem_praha).Index(i++);
			Map(m => m.rezervace).Index(i++);
			Map(m => m.cena_nakup).Index(i++);
			Map(m => m.pocetcc).Index(i++);
			Map(m => m.kusupozadujeme).Index(i++);
			Map(m => m.zobrazovat_max).Index(i++);
			Map(m => m.carovy_kod).Index(i++);
			Map(m => m.timemark).Index(i++);
			Map(m => m.sklad).Index(i++);
			Map(m => m.v_boosteru).Index(i++);
			Map(m => m.problemova).Index(i++);
			Map(m => m.reserved_list).Index(i++);
			Map(m => m.spolecna).Index(i++);
			Map(m => m.barva_razeni).Index(i++);
			Map(m => m.mkm_id).Index(i++);
			Map(m => m.typ_precenovani).Index(i++);
			Map(m => m.smazat).Index(i++);
			Map(m => m.pultovka).Index(i++);
			Map(m => m.katalog).Index(i++);
			Map(m => m.vykup_mailem).Index(i++);
		}
	}
}
