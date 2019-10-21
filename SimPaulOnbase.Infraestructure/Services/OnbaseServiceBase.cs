using Hyland.Unity;
using Hyland.Unity.UnityForm;
using SimPaulOnbase.Core.Exceptions;
using System;
using System.IO;

namespace SimPaulOnbase.Infraestructure.Services
{
    /// <summary>
    /// OnbaseIntegrationBase class
    /// </summary>
    public class OnbaseServiceBase
    {
        /// <summary>
        /// Onbase Unity Application Instance
        /// </summary>
        protected Hyland.Unity.Application unityApplication;

        /// <summary>
        /// OnbaseSettings Instance
        /// </summary>
        protected OnbaseSettings onbaseSettings;

        public OnbaseServiceBase(OnbaseSettings onbaseSettings)
        {
            this.onbaseSettings = onbaseSettings;
        }

        /// <summary>
        /// Init new Onbase Form Integration and retun StoreNewUnityFormProperties to set fields and values
        /// </summary>
        /// <param name="onBaseFormID">Onbase Form Template ID</param>
        protected StoreNewUnityFormProperties InitNewForm(FormTemplate formTemplate)
        {
            var store = this.CreateNewStoreUnityForm(formTemplate);
            return store;
        }


        /// <summary>
        /// Connect to Onbase App Server 
        /// </summary>
        private void Connect()
        {

            var authProps = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(onbaseSettings.AppServerURL, onbaseSettings.Username, onbaseSettings.Password, onbaseSettings.DataSource);
            authProps.LicenseType = LicenseType.QueryMetering;

            try
            {
                unityApplication = Hyland.Unity.Application.Connect(authProps);
            }
            catch (MaxLicensesException)
            {
                throw new OnbaseConnectionException("Error: All available licenses have been consumed.");
            }
            catch (SystemLockedOutException)
            {
                throw new OnbaseConnectionException("Error: The system is currently in lockout mode.");
            }
            catch (InvalidLoginException)
            {
                throw new OnbaseConnectionException("Error: Invalid Login Credentials.");
            }          
            catch (AuthenticationFailedException)
            {
                throw new OnbaseConnectionException("Error: NT Authentication Failed.");
            }
            catch (MaxConcurrentLicensesException)
            {
                throw new OnbaseConnectionException("Error: All concurrent licenses for this user group have been consumed.");
            }
            catch (InvalidLicensingException)
            {
                throw new OnbaseConnectionException("Error: Invalid Licensing.");
            }
        }

        /// <summary>
        /// Update a image file image to form document
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keywordType"></param>
        /// <param name="keywordValue"></param>
        public void UploadUnityFormImage(string filePath, string keywordType, string keywordValue)
        {
            using (PageData pageData = this.unityApplication.Core.Storage.CreatePageData(new MemoryStream(Convert.FromBase64String(filePath)), ".jpg"))
            {
                DocumentType docType = this.unityApplication.Core.DocumentTypes.Find(onbaseSettings.CustomerDocumentType);
                FileType img = this.unityApplication.Core.FileTypes.Find(onbaseSettings.CustomerDocumentFileType);

                StoreNewDocumentProperties newDocProps = this.unityApplication.Core.Storage.CreateStoreNewDocumentProperties(docType, img);
                KeywordType idAnexoType = this.unityApplication.Core.KeywordTypes.Find(keywordType);
                Keyword idanexo = idAnexoType.CreateKeyword(keywordValue);
                newDocProps.AddKeyword(idanexo);

                Document newDoc = this.unityApplication.Core.Storage.StoreNewDocument(pageData, newDocProps);
            }
        }

        /// <summary>
        /// Find form tempalte by id
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        protected FormTemplate FindFormTemplate(long formID)
        {
            this.Connect();
            FormTemplate formTemplate = this.unityApplication.Core.UnityFormTemplates.Find(formID);

            if (formTemplate == null)
            {
                throw new ApplicationException($"From template with ID {formID} not found.");
            }

            return formTemplate;
        }

        /// <summary>
        /// Create a new Unity Form Store Properties
        /// </summary>
        /// <param name="formTemplate"></param>
        /// <returns></returns>
        private StoreNewUnityFormProperties CreateNewStoreUnityForm(FormTemplate formTemplate)
        {
            StoreNewUnityFormProperties properties = this.unityApplication.Core.Storage.CreateStoreNewUnityFormProperties(formTemplate);

            if (properties == null)
            {
                throw new ApplicationException("Could't create a new Onbase Store Form Properties.");
            }

            return properties;
        }

        /// <summary>
        /// Store a form to Unity application
        /// </summary>
        /// <param name="storeNew"></param>
        /// <returns></returns>
        protected Document StoreNewUnityForm(StoreNewUnityFormProperties storeNew)
        {
            return this.unityApplication.Core.Storage.StoreNewUnityForm(storeNew);
        }
    }
}
