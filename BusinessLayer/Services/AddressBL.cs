using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public AddAddress AddAddress(AddAddress addAddress, int userId)
        {
            try
            {
                return addressRL.AddAddress(addAddress, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressModel UpdateAddress(AddressModel addressModel, int userId)
        {
            try
            {
                return addressRL.UpdateAddress(addressModel, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteAddress(int addressId, int userId)
        {
            try
            {
                return addressRL.DeleteAddress(addressId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressModel GetAddressById(int typeId, int addressId, int userId)
        {
            try
            {
                return addressRL.GetAddressById(typeId, addressId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressModel> GetAllAddresses(int userId)
        {
            try
            {
                return addressRL.GetAllAddresses(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
