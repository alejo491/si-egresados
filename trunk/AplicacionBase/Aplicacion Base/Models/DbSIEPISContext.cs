using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Aplicacion_Base.Models.Mapping;

namespace Aplicacion_Base.Models
{
    public partial class DbSIEPISContext : DbContext
    {
        static DbSIEPISContext()
        {
            Database.SetInitializer<DbSIEPISContext>(null);
        }

        public DbSIEPISContext()
            : base("Name=DbSIEPISContext")
        {
        }

        public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Ejemplare> Ejemplares { get; set; }
        public DbSet<EjemplarPregunta> EjemplarPreguntas { get; set; }
        public DbSet<Electiva> Electivas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Encuestado> Encuestados { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<ExpJefe> ExpJefes { get; set; }
        public DbSet<ExpLaborale> ExpLaborales { get; set; }
        public DbSet<Institucione> Instituciones { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Jefe> Jefes { get; set; }
        public DbSet<OpcionesdeRespuesta> OpcionesdeRespuestas { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vacante> Vacantes { get; set; }
        public DbSet<vw_aspnet_Applications> vw_aspnet_Applications { get; set; }
        public DbSet<vw_aspnet_MembershipUsers> vw_aspnet_MembershipUsers { get; set; }
        public DbSet<vw_aspnet_Profiles> vw_aspnet_Profiles { get; set; }
        public DbSet<vw_aspnet_Roles> vw_aspnet_Roles { get; set; }
        public DbSet<vw_aspnet_Users> vw_aspnet_Users { get; set; }
        public DbSet<vw_aspnet_UsersInRoles> vw_aspnet_UsersInRoles { get; set; }
        public DbSet<vw_aspnet_WebPartState_Paths> vw_aspnet_WebPartState_Paths { get; set; }
        public DbSet<vw_aspnet_WebPartState_Shared> vw_aspnet_WebPartState_Shared { get; set; }
        public DbSet<vw_aspnet_WebPartState_User> vw_aspnet_WebPartState_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new aspnet_ApplicationsMap());
            modelBuilder.Configurations.Add(new aspnet_MembershipMap());
            modelBuilder.Configurations.Add(new aspnet_PathsMap());
            modelBuilder.Configurations.Add(new aspnet_PersonalizationAllUsersMap());
            modelBuilder.Configurations.Add(new aspnet_PersonalizationPerUserMap());
            modelBuilder.Configurations.Add(new aspnet_ProfileMap());
            modelBuilder.Configurations.Add(new aspnet_RolesMap());
            modelBuilder.Configurations.Add(new aspnet_SchemaVersionsMap());
            modelBuilder.Configurations.Add(new aspnet_UsersMap());
            modelBuilder.Configurations.Add(new aspnet_WebEvent_EventsMap());
            modelBuilder.Configurations.Add(new ComentarioMap());
            modelBuilder.Configurations.Add(new CuentaMap());
            modelBuilder.Configurations.Add(new EjemplareMap());
            modelBuilder.Configurations.Add(new EjemplarPreguntaMap());
            modelBuilder.Configurations.Add(new ElectivaMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new EncuestadoMap());
            modelBuilder.Configurations.Add(new EncuestaMap());
            modelBuilder.Configurations.Add(new EstudioMap());
            modelBuilder.Configurations.Add(new ExpJefeMap());
            modelBuilder.Configurations.Add(new ExpLaboraleMap());
            modelBuilder.Configurations.Add(new InstitucioneMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new JefeMap());
            modelBuilder.Configurations.Add(new OpcionesdeRespuestaMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new PreguntaMap());
            modelBuilder.Configurations.Add(new ReporteMap());
            modelBuilder.Configurations.Add(new RespuestaMap());
            modelBuilder.Configurations.Add(new TemaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new VacanteMap());
            modelBuilder.Configurations.Add(new vw_aspnet_ApplicationsMap());
            modelBuilder.Configurations.Add(new vw_aspnet_MembershipUsersMap());
            modelBuilder.Configurations.Add(new vw_aspnet_ProfilesMap());
            modelBuilder.Configurations.Add(new vw_aspnet_RolesMap());
            modelBuilder.Configurations.Add(new vw_aspnet_UsersMap());
            modelBuilder.Configurations.Add(new vw_aspnet_UsersInRolesMap());
            modelBuilder.Configurations.Add(new vw_aspnet_WebPartState_PathsMap());
            modelBuilder.Configurations.Add(new vw_aspnet_WebPartState_SharedMap());
            modelBuilder.Configurations.Add(new vw_aspnet_WebPartState_UserMap());
        }
    }
}
