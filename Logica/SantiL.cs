using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esmeralda02.Logica
{
    public class SantiL
    {
        public List<Mascota> MtBuscarMascotas(string texto)
        {
            List<Mascota> oBusquedadMascotas = new List<Mascota>();
            using (SqlConnection cn = ConexionDB.MtAbrirConexion())
            {
                cn.Open();
                string query = @"
                    SELECT 
                        m.Id,
                        m.NombreMascota,
                        m.Especie,
                        m.Raza,
                        m.Sexo,
                        m.Edad,
                        p.Nombre + ' ' + p.Apellido AS Propietario
                    FROM Mascota m
                    INNER JOIN Persona p ON m.IdPersona = p.Id
                    WHERE m.NombreMascota LIKE @buscar OR p.Nombre LIKE @buscar";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@buscar", "%" + texto + "%");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            Mascota oMascota = new Mascota()
                            {

                                Id = Convert.ToInt32(dr["Id"]),
                                NombreMascota = dr["NombreMascota"].ToString(),
                                Especie = dr["Especie"].ToString(),
                                Raza = dr["Raza"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                Edad = Convert.ToInt32(dr["Edad"]),

                                Persona = new Persona
                                {
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString()
                                }

                            };
                            oBusquedadMascotas.Add(oMascota);
                        }
                        return oBusquedadMascotas;
                    }
                }
            }
        }
    }
}




  