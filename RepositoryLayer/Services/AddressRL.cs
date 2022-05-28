using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly IConfiguration configuration;

        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AddAddress AddAddress(AddAddress addAddress, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Address", addAddress.Address);
                    cmd.Parameters.AddWithValue("@City", addAddress.City);
                    cmd.Parameters.AddWithValue("@State", addAddress.State);
                    cmd.Parameters.AddWithValue("@TypeId", addAddress.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return addAddress;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return addressModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        public string DeleteAddress(int addressId, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Address Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete Address";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public AddressModel GetAddressById(int addressId, int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    AddressModel addressResponse = new AddressModel();
                    SqlCommand cmd = new SqlCommand("spGetAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AddressId", addressId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            AddressModel address = new AddressModel();
                            addressResponse = ReadData(address, rdr);
                        }
                        con.Close();
                        return addressResponse;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public AddressModel ReadData(AddressModel address, SqlDataReader rdr)
        {
            address.Address = Convert.ToString(rdr["Address"] == DBNull.Value ? default : rdr["Address"]);
            address.City = Convert.ToString(rdr["City"] == DBNull.Value ? default : rdr["City"]);
            address.State = Convert.ToString(rdr["State"] == DBNull.Value ? default : rdr["State"]);
            address.TypeId = Convert.ToInt32(rdr["TypeId"] == DBNull.Value ? default : rdr["TypeId"]);
            address.AddressId = Convert.ToInt32(rdr["AddressId"] == DBNull.Value ? default : rdr["AddressId"]);
            
            return address;
        }

        public List<AddressModel> GetAllAddresses(int userId)
        {
            using (SqlConnection con = new SqlConnection(configuration["ConnectionString:BookStore"]))
            {
                try
                {
                    List<AddressModel> addressResponse = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("spGetAllAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            AddressModel address = new AddressModel();
                            AddressModel temp;
                            temp = ReadData(address, rdr);
                            addressResponse.Add(temp);
                        }
                        con.Close();
                        return addressResponse;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
