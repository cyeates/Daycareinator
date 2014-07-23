﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Daycareinator.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class EmailResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmailResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Daycareinator.Resources.EmailResources", typeof(EmailResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;We have received a request to change your password.  If you made this request, please click the link below.&lt;/p&gt;&lt;p&gt;{0}&lt;/p&gt;&lt;p&gt;This link will be valid for 24 hours, or until you change your password.&lt;/p&gt;.
        /// </summary>
        public static string ForgotPasswordBody {
            get {
                return ResourceManager.GetString("ForgotPasswordBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Walden CPB password change request.
        /// </summary>
        public static string ForgotPasswordSubject {
            get {
                return ResourceManager.GetString("ForgotPasswordSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You&apos;re invited to join Walden CPB.  All you need to do is choose a password.  It only takes a few seconds.  Click this link to get started: &lt;/br&gt; {0}.
        /// </summary>
        public static string InvitationEmailBody {
            get {
                return ResourceManager.GetString("InvitationEmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You&apos;re invited to join Walden CPB.
        /// </summary>
        public static string InvitationEmailSubject {
            get {
                return ResourceManager.GetString("InvitationEmailSubject", resourceCulture);
            }
        }
    }
}