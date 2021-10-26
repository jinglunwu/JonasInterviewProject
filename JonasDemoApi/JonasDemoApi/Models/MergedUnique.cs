using System.ComponentModel.DataAnnotations;

namespace JonasDemoApi.Models
{
    public class MergedUnique
    {
        [Required(ErrorMessage = "UNITID is required")]
        [Key]
        public string UNITID { get; set; }
        public string OPEID { get; set; }
        public string OPEID6 { get; set; }
        public string INSTNM { get; set; }
        public string CITY { get; set; }
        public string STABBR { get; set; }
        public string ZIP { get; set; }
        public string ACCREDAGENCY { get; set; }
        public string INSTURL { get; set; }
        public string NPCURL { get; set; }
        public string SCH_DEG { get; set; }
        public string HCM2 { get; set; }
        public string MAIN { get; set; }
        public string NUMBRANCH { get; set; }
        public string PREDDEG { get; set; }
        public string HIGHDEG { get; set; }
        public string CONTROL { get; set; }
        public string ST_FIPS { get; set; }
        public string REGION { get; set; }
        public string LOCALE { get; set; }
        public string LOCALE2 { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string CCBASIC { get; set; }
        public string CCUGPROF { get; set; }
        public string CCSIZSET { get; set; }
        public string HBCU { get; set; }
        public string PBI { get; set; }
        public string ANNHI { get; set; }
        public string TRIBAL { get; set; }
        public string AANAPII { get; set; }
        public string HSI { get; set; }
        public string NANTI { get; set; }
        public string MENONLY { get; set; }
        public string WOMENONLY { get; set; }
        public string RELAFFIL { get; set; }
        public string ADM_RATE { get; set; }
        public string ADM_RATE_ALL { get; set; }
        public string SATVR25 { get; set; }
        public string SATVR75 { get; set; }
        public string SATMT25 { get; set; }
        public string SATMT75 { get; set; }
        public string SATWR25 { get; set; }
        public string SATWR75 { get; set; }
        public string SATVRMID { get; set; }
        public string SATMTMID { get; set; }
        public string SATWRMID { get; set; }
        public string ACTCM25 { get; set; }
        public string ACTCM75 { get; set; }
        public string ACTEN25 { get; set; }
        public string ACTEN75 { get; set; }
        public string ACTMT25 { get; set; }
        public string ACTMT75 { get; set; }
        public string ACTWR25 { get; set; }
        public string ACTWR75 { get; set; }
        public string ACTCMMID { get; set; }
        public string ACTENMID { get; set; }
        public string ACTMTMID { get; set; }
        public string ACTWRMID { get; set; }
        public string SAT_AVG { get; set; }
        public string SAT_AVG_ALL { get; set; }
        public string PCIP01 { get; set; }
        public string PCIP03 { get; set; }
        public string PCIP04 { get; set; }
        public string PCIP05 { get; set; }
        public string PCIP09 { get; set; }
        public string PCIP10 { get; set; }
        public string PCIP11 { get; set; }
        public string PCIP12 { get; set; }
        public string PCIP13 { get; set; }
        public string PCIP14 { get; set; }
        public string PCIP15 { get; set; }
        public string PCIP16 { get; set; }
        public string PCIP19 { get; set; }
        public string PCIP22 { get; set; }
        public string PCIP23 { get; set; }
        public string PCIP24 { get; set; }
        public string PCIP25 { get; set; }
        public string PCIP26 { get; set; }
        public string PCIP27 { get; set; }
        public string PCIP29 { get; set; }
        public string PCIP30 { get; set; }
        public string PCIP31 { get; set; }
        public string PCIP38 { get; set; }
        public string PCIP39 { get; set; }
        public string PCIP40 { get; set; }
        public string PCIP41 { get; set; }
        public string PCIP42 { get; set; }
        public string PCIP43 { get; set; }
        public string PCIP44 { get; set; }
        public string PCIP45 { get; set; }
        public string PCIP46 { get; set; }
        public string PCIP47 { get; set; }
        public string PCIP48 { get; set; }
        public string PCIP49 { get; set; }
        public string PCIP50 { get; set; }
        public string PCIP51 { get; set; }
        public string PCIP52 { get; set; }
        public string PCIP54 { get; set; }
        public string CIP01CERT1 { get; set; }
        public string CIP01CERT2 { get; set; }
        public string CIP01ASSOC { get; set; }

    }
}