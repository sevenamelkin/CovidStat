using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CovidStat.Db.Entities
{
	/// <summary>
	/// Описание сущности для таблицы
	/// </summary>
	[Table("ip2location", Schema = "public")]
    public class IpLocation
    {
	    /// <summary>
	    /// Значение минимального диапозона для страны
	    /// </summary>
	    [Key]
	    [Column("ip_from")]
	    public long IpFrom {get;set;}
	    
	    /// <summary>
	    /// Значение максимального диапозона для страны
	    /// </summary>
	    [Key]
	    [Column("ip_to")]
	    public long IpTo {get;set;}
	    
	    /// <summary>
	    /// Краткий код страны
	    /// </summary>
	    [Column("country_code")]
	    public string CountryCode {get;set;}
	    
	    /// <summary>
	    /// Полное название страны
	    /// </summary>
	    [Column("country_name")]
	    public string CountryName {get;set;}
    }
}