﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using GatewayApiClient.DataContracts.EnumTypes;
using GatewayApiClient.Utility;

namespace GatewayApiClient.DataContracts {

    [DataContract(Namespace = "")]
    public class GetBuyerData : BaseResponse {

        [DataMember(EmitDefaultValue = false)]
        public Collection<BuyerAddress> AddressCollection { get; set; }

        #region BuyerCategory

        /// <summary>
        /// Categoria do comprador
        /// </summary>
        [DataMember(Name = "BuyerCategory", EmitDefaultValue = false)]
        private string BuyerCategoryField
        {
            get
            {
                if (this.BuyerCategory == null) { return null; }
                return this.BuyerCategory.ToString();
            }
            set
            {
                if (value == null) {
                    this.BuyerCategory = null;
                } else {
                    this.BuyerCategory = (BuyerCategoryEnum)Enum.Parse(typeof(BuyerCategoryEnum), value);
                }
            }
        }

        /// <summary>
        /// Categoria do comprador
        /// </summary>
        public Nullable<BuyerCategoryEnum> BuyerCategory { get; set; }

        #endregion

        [DataMember(EmitDefaultValue = false)]
        public Guid BuyerKey { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BuyerReference { get; set; }

        #region CreateDateInMerchant

        /// <summary>
        /// Data de criação do comprador no sistema da loja
        /// </summary>
        [DataMember(Name = "CreateDateInMerchant", EmitDefaultValue = false)]
        private string CreateDateInMerchantField
        {
            get
            {
                if (this.CreateDateInMerchant == null) { return null; }
                return this.CreateDateInMerchant.Value.ToString(ServiceConstants.DATE_TIME_FORMAT);
            }
            set
            {
                if (value == null) {
                    this.CreateDateInMerchant = null;
                } else {
                    this.CreateDateInMerchant = DateTime.ParseExact(value, ServiceConstants.DATE_TIME_FORMAT, null);
                }
            }
        }

        /// <summary>
        /// Data de criação do comprador no sistema da loja
        /// </summary>
        public Nullable<DateTime> CreateDateInMerchant { get; set; }

        #endregion

        #region LastBuyerUpdateInMerchant

        /// <summary>
        /// Data da última atualização do cadastro do comprador no sistema da loja
        /// </summary>
        [DataMember(Name = "LastBuyerUpdateInMerchant", EmitDefaultValue = false)]
        private string LastBuyerUpdateInMerchantField
        {
            get
            {
                if (this.LastBuyerUpdateInMerchant == null) { return null; }
                return this.LastBuyerUpdateInMerchant.Value.ToString(ServiceConstants.DATE_TIME_FORMAT);
            }
            set
            {
                if (value == null) {
                    this.LastBuyerUpdateInMerchant = null;
                } else {
                    this.LastBuyerUpdateInMerchant = DateTime.ParseExact(value, ServiceConstants.DATE_TIME_FORMAT, null);
                }
            }
        }

        /// <summary>
        /// Data da última atualização do cadastro do comprador no sistema da loja
        /// </summary>
        public Nullable<DateTime> LastBuyerUpdateInMerchant { get; set; }

        #endregion

        [DataMember(EmitDefaultValue = false)]
        public bool Success { get; set; }

        /// <summary>
        /// Nome da pessoa
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        #region PersonType

        /// <summary>
        /// Define se é pessoa física ou jurídica
        /// </summary>
        [DataMember(Name = "PersonType")]
        private string PersonTypeField
        {
            get
            {
                return this.PersonType.ToString();
            }
            set
            {
                this.PersonType = (PersonTypeEnum)Enum.Parse(typeof(PersonTypeEnum), value);
            }
        }

        /// <summary>
        /// Define se é pessoa física ou jurídica
        /// </summary>
        public PersonTypeEnum PersonType { get; set; }

        #endregion

        /// <summary>
        /// Número do documento
        /// </summary>
        [DataMember]
        public string DocumentNumber { get; set; }

        #region DocumentType

        /// <summary>
        /// Tipo de documento
        /// </summary>
        [DataMember(Name = "DocumentType")]
        private string DocumentTypeField
        {
            get
            {
                return this.DocumentType.ToString();
            }
            set
            {
                this.DocumentType = (DocumentTypeEnum)Enum.Parse(typeof(DocumentTypeEnum), value);
            }
        }

        /// <summary>
        /// Tipo de documento
        /// </summary>
        public DocumentTypeEnum DocumentType { get; set; }

        #endregion

        #region Gender

        /// <summary>
        /// Sexo da pessoa
        /// </summary>
        [DataMember(Name = "Gender", EmitDefaultValue = false)]
        private string GenderField
        {
            get
            {
                if (this.Gender == null) { return null; }
                return Gender.ToString();
            }
            set
            {
                if (value == null) {
                    this.Gender = null;
                } else {
                    this.Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), value);
                }
            }
        }

        /// <summary>
        /// Sexo da pessoa
        /// </summary>
        public Nullable<GenderEnum> Gender { get; set; }

        #endregion

        #region Birthdate

        /// <summary>
        /// Data de nascimento
        /// </summary>
        [DataMember(Name = "Birthdate", EmitDefaultValue = false)]
        private string BirthdateField
        {
            get
            {
                if (this.Birthdate == null) { return null; }
                DateTime birthdate;
                TryParseDate(this.Birthdate.ToString(), out birthdate, ServiceConstants.DATE_FORMAT, ServiceConstants.DATE_TIME_FORMAT);
                return birthdate.ToString();
            }
            set
            {
                if (value == null) {
                    this.Birthdate = null;
                } else {
                    DateTime birthdate;
                    TryParseDate(value, out birthdate, ServiceConstants.DATE_FORMAT, ServiceConstants.DATE_TIME_FORMAT);
                    this.Birthdate = birthdate;
                }
            }
        }

        private bool TryParseDate(string value, out DateTime dateTime, params string[] formatCollection)
        {
            if (formatCollection == null) { throw new ArgumentNullException("formatCollection"); }
            if (formatCollection.Length == 0) { throw new ArgumentException("At least one format must be specified.", "formats"); }

            foreach (string format in formatCollection)
            {
                if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out dateTime) == true)
                {
                    return true;
                }
            }

            dateTime = DateTime.MinValue;
            return false;
        }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        [IgnoreDataMember]
        public Nullable<DateTime> Birthdate { get; set; }

        #endregion

        /// <summary>
        /// E-mail
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        #region EmailType

        /// <summary>
        /// Tipo de e-mail. Pessoal ou comercial
        /// </summary>
        [DataMember(Name = "EmailType")]
        private string EmailTypeField
        {
            get
            {
                return this.EmailType.ToString();
            }
            set
            {
                this.EmailType = (EmailTypeEnum)Enum.Parse(typeof(EmailTypeEnum), value);
            }
        }

        /// <summary>
        /// Tipo de e-mail. Pessoal ou comercial
        /// </summary>
        public EmailTypeEnum EmailType { get; set; }

        #endregion

        /// <summary>
        /// Código identificador do cadsastro do Facebook
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string FacebookId { get; set; }

        /// <summary>
        /// Código identificador do cadastro do Twitter
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string TwitterId { get; set; }

        /// <summary>
        /// Telefone celular
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Telefone residencial
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string HomePhone { get; set; }

        /// <summary>
        /// Telefone comercial
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string WorkPhone { get; set; }
    }
}
