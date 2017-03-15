using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Sample.Domain.Models;

namespace Sample.Datastore.Mappings
{
	public class AnimalMappings : EntityTypeConfiguration<Animal>
	{
		public AnimalMappings()
		{
			ToTable("Animal");
			HasKey(x => x.AnimalKey);

			Property(x => x.Sex).HasColumnName("SexId").IsRequired();
			Property(x => x.Species).HasColumnName("SpeciesId").IsRequired();
			Property(x => x.UpdateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
		}
	}
}