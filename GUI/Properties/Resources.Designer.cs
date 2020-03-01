﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GUI.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to V průběhu automatického načtení dat při startu programu, nastala neočekávaná chyba. Je možné, že je poškozený datový soubor. Zkuste tento soubor přejmenovat nebo odstranit. Nachází se v adresáři s polečně s tímto programem. .
        /// </summary>
        internal static string AutoImportError {
            get {
                return ResourceManager.GetString("AutoImportError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chyba automatickkého načtení dat.
        /// </summary>
        internal static string AutoImportErrorTitle {
            get {
                return ResourceManager.GetString("AutoImportErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Soubor s daty pro automatické načtení již existuje. Chcete ho prepsat?.
        /// </summary>
        internal static string AutoloadFileExists {
            get {
                return ResourceManager.GetString("AutoloadFileExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Soubor již existuje.
        /// </summary>
        internal static string AutoloadFileExistsTitle {
            get {
                return ResourceManager.GetString("AutoloadFileExistsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to V programu již existuji data, chcete je přapsat?.
        /// </summary>
        internal static string DataAlreadyExists {
            get {
                return ResourceManager.GetString("DataAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Existující data.
        /// </summary>
        internal static string DataAlreadyExistsTitle {
            get {
                return ResourceManager.GetString("DataAlreadyExistsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Při ukládaní dat se vyskytla chyba!.
        /// </summary>
        internal static string DataSaveError {
            get {
                return ResourceManager.GetString("DataSaveError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chyba.
        /// </summary>
        internal static string ErrorLabel {
            get {
                return ResourceManager.GetString("ErrorLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to V průběhu exportování se vyskytla chyba:
        ///
        ///.
        /// </summary>
        internal static string ExportError {
            get {
                return ResourceManager.GetString("ExportError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chyba exportu.
        /// </summary>
        internal static string ExportErrorTitle {
            get {
                return ResourceManager.GetString("ExportErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to V průběhu importování se vyskytla chyba:
        ///
        ///.
        /// </summary>
        internal static string ImportError {
            get {
                return ResourceManager.GetString("ImportError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chyba importu.
        /// </summary>
        internal static string ImportErrorTitle {
            get {
                return ResourceManager.GetString("ImportErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neexistuje soubor pro import!.
        /// </summary>
        internal static string ImportFileNotExists {
            get {
                return ResourceManager.GetString("ImportFileNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap InfoPanel {
            get {
                object obj = ResourceManager.GetObject("InfoPanel", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chcete otevřít soubor s uloženými daty?.
        /// </summary>
        internal static string OpenDataFile {
            get {
                return ResourceManager.GetString("OpenDataFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chcete otevřít soubor s uloženými daty?.
        /// </summary>
        internal static string OpenDataFileQuestion {
            get {
                return ResourceManager.GetString("OpenDataFileQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Otevřít soubor.
        /// </summary>
        internal static string OpenFileTitle {
            get {
                return ResourceManager.GetString("OpenFileTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cesta vede do stejného vrcholu!.
        /// </summary>
        internal static string PathToSameVertex {
            get {
                return ResourceManager.GetString("PathToSameVertex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zvolené vrcholy v grafu neexistují!.
        /// </summary>
        internal static string SelectedVerticesNotExist {
            get {
                return ResourceManager.GetString("SelectedVerticesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neuložené změny.
        /// </summary>
        internal static string UnsavedChanges {
            get {
                return ResourceManager.GetString("UnsavedChanges", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Máte neuložené změny grafu, chcete je uložit?.
        /// </summary>
        internal static string UnsavedChangesQuestionToSave {
            get {
                return ResourceManager.GetString("UnsavedChangesQuestionToSave", resourceCulture);
            }
        }
    }
}
