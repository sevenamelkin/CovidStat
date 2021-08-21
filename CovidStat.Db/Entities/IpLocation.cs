using System.ComponentModel.DataAnnotations.Schema;

namespace CovidStat.Db.Entities
{
	/// <summary>
	/// Описание сущности для таблицы
	/// </summary>
	[Table("messages")]
    public class IpLocation
    {
	    /// <summary>
	    /// Значение минимального диапозона для страны
	    /// </summary>
	    [Column("ip_from")]
	    public long IpFrom {get;set;}
	    
	    /// <summary>
	    /// Значение максимального диапозона для страны
	    /// </summary>
	    [Column("ip_from")]
	    public long IpTo {get;set;}
	    
	    /// <summary>
	    /// Краткий код страны
	    /// </summary>
	    [Column("ip_from")]
	    public string CountryCode {get;set;}
	    
	    /// <summary>
	    /// Полное название страны
	    /// </summary>
	    [Column("ip_from")]
	    public string CountryName {get;set;}
    }
}