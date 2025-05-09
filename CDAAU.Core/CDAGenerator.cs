using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.HL7.CDA;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace TeamUnicorn.CDAAU.Core
{

    public class CDAGenerator
    {

        static NeHTALevelAttribute.NeHTALevels GenerateLevel = NeHTALevelAttribute.NeHTALevels.Level3a;

        static TimeZoneInfo timezoneinfo = null;

        public static POCD_MT000040ClinicalDocument Generate(object data, string timezone)
        {
            GenerateLevel = NeHTALevelAttribute.NeHTALevels.Level3a;

            if (string.IsNullOrEmpty(timezone))
                timezoneinfo = TimeZoneInfo.Local;
            else
                timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);

            return RIMActFactory(null, data) as POCD_MT000040ClinicalDocument;
        }

        public static POCD_MT000040ClinicalDocument Generate(object data, string timezone, NeHTALevelAttribute.NeHTALevels outputlevel)
        {
            GenerateLevel = outputlevel;

            if (string.IsNullOrEmpty(timezone))
                timezoneinfo = TimeZoneInfo.Local;
            else
                timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);

            return RIMActFactory(null, data) as POCD_MT000040ClinicalDocument;
        }
        

        public static object RIMActFactory(dynamic parentrelationship, object data)
        {
            // create object
            string classcode = data.GetAttributeValue<RIMActAttribute, string>(a => a.ClassCode);
            string moodcode = data.GetAttributeValue<RIMActAttribute, string>(a => a.MoodCode);
            Type acttype = data.GetAttributeValue<RIMActAttribute, Type>(a => a.ElementType);

            if (acttype == null)
            {
                classcode = data.GetAttributeValue<RIMRoleAttribute, string>(a => a.ClassCode);
                acttype = data.GetAttributeValue<RIMRoleAttribute, Type>(a => a.ElementType);
            }
                        
            if (acttype == null)
                return null;

            dynamic output = Activator.CreateInstance(acttype);

            // set fixed classCode
            AssignToStringOrEnum("classCode", output, classcode);
           
            // set fixed moodCode
            if( moodcode != null &&  output.GetType().GetProperty("moodCode") != null )
                AssignToStringOrEnum("moodCode", output, moodcode);

            // template, type etc.

            if (output.GetType().GetProperty("typeId") != null)
                output.typeId = data.GetAttributeValueII<RIMTypeIdAttribute, POCD_MT000040InfrastructureRoottypeId>();

            if (output.GetType().GetProperty("templateId") != null)
            {
                output.templateId = data.GetAttributeValuesII<RIMTemplateIdAttribute, II>();
                if( output.templateId != null )
                    output.templateId = new List<II>(output.templateId).OrderByDescending(x => x.root).ToArray();
            }

            // set fixed act code
            string code = data.GetAttributeValue<RIMActCodeAttribute, string>(a => a.Code);
            string codeSystem = data.GetAttributeValue<RIMActCodeAttribute, string>(a => a.CodeSystem);
            if (code != null && codeSystem != null)
            {
                string displayName = data.GetAttributeValue<RIMActCodeAttribute, string>(a => a.DisplayName);
                string codeSystemName = data.GetAttributeValue<RIMActCodeAttribute, string>(a => a.CodeSystemName);

                AssignToCode(output, code, codeSystem, codeSystemName, displayName);
            }

            // set fixed title
            string title = data.GetAttributeValue<RIMActTitleAttribute, string>(a => a.Title);
            if (title != null)
                output.title = new ST() { Text = new string[] { title } };


            foreach (System.Reflection.PropertyInfo pinfo in data.GetType().GetProperties())
            {

                NeHTALevelAttribute.NeHTALevels? nehtalevel = pinfo.GetAttributeValue<NeHTALevelAttribute, NeHTALevelAttribute.NeHTALevels?>(a => a.Level);
                if (nehtalevel != null & nehtalevel.HasValue && (int)nehtalevel.Value > (int)GenerateLevel)
                    continue;

                object item = output;

                // referenced child entity - switch context to entity
                Type entitytype = pinfo.GetAttributeValue<RIMEntityAttribute, Type>(a => a.ElementType);
                string entityelement = pinfo.GetAttributeValue<RIMEntityAttribute, string>(a => a.ElementName);

                if (entitytype != null)
                {
                    // get current value of reference entity if it exists
                    System.Reflection.PropertyInfo entityinfo = output.GetType().GetProperty(entityelement);
                    dynamic current = entityinfo.GetValue(output, null);
                    if (current == null)
                    {
                        current = Activator.CreateInstance(entitytype);
                        entityinfo.SetValue(output, current, null);
                    }

                    // use the entity to set the attribute value
                    item = current;
                }

                // walk the attributes
                RIMAttributeType rimattr = pinfo.GetAttributeValue<RIMAttributeAttribute, RIMAttributeType>(a => a.Name);
                                
                if (rimattr != RIMAttributeType.Ignore)
                {
                    dynamic v = pinfo.GetValue(data, null);

                    if (v == null)
                        continue;

                    System.Reflection.PropertyInfo info = item.GetType().GetProperty(rimattr.ToString().ToFirstLower());

                    switch (rimattr)
                    {
                        case RIMAttributeType.MoodCode:

                            if (v is string)
                                AssignToStringOrEnum("moodCode", item, v);
                            else
                            {
                                string gcode = (v as Enum).GetAttributeValue<CodedAttribute, string>(a => a.Code);
                                if (gcode != null)
                                    AssignToStringOrEnum("moodCode", item, gcode);
                            }

                            break;

                        case RIMAttributeType.Id:
                            if (v is identifier)
                            {
                                AssignValueOrArray(info, item, new II() { root = v.Root, extension = v.Extension, assigningAuthorityName = v.AuthorityName });
                            }
                            else
                            {
                                string idroot = pinfo.GetAttributeValue<RIMAttributeAttribute, string>(a => a.IdRoot);
                                AssignValueOrArray(info, item, new II() { root = ((idroot == null) ? v.ToString() : idroot), extension = ((idroot == null) ? null : v.ToString()) });
                            }
                            break;

                        case RIMAttributeType.SetId:
                            string setidroot = pinfo.GetAttributeValue<RIMAttributeAttribute, string>(a => a.IdRoot);
                            AssignValueOrArray(info, item, new II() { root = ((setidroot == null) ? v.ToString() : setidroot), extension = ((setidroot == null) ? null : v.ToString()) });
                            break;

                        case RIMAttributeType.VersionNumber:
                            AssignValueOrArray(info, item, new INT() { value = v.ToString() });
                            break;

                        case RIMAttributeType.ConfidentialityCode:

                            if (v is string)
                                AssignValueOrArray(info, item, new CS() { code = v.ToString() });
                            else if (v is codeable)
                                AssignToCode(info, item, v);
                            {
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;

                        case RIMAttributeType.LanguageCode:

                            if (v is string)
                                AssignValueOrArray(info, item, new CS() { code = v.ToString() });
                            else if (v is codeable)
                                AssignToCode(info, item, v);

                            {
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;

                        case RIMAttributeType.CompletionCode:
                             if (v is codeable)
                                AssignToCode(info, item, v);
                            {
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;

                        case RIMAttributeType.Code:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else
                            {
                                // try coded enum
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                                else
                                {
                                    dynamic o = Activator.CreateInstance(info.PropertyType);
                                    o.originalText = new ED() { Text = new string[] { v.ToString() } };
                                    AssignValueOrArray(info, item, o);
                                }
                            }
                            break;

                        case RIMAttributeType.Addr:
                            AddressUseType ause = pinfo.GetAttributeValue<RIMAttributeAttribute, AddressUseType>(a => a.AddressUse);  
                           
                            if (v is address)
                                AssignValueOrArray(info, item, AddrValue(v, ause));
                            break;

                        case RIMAttributeType.Telecom:
                            TEL t = new TEL();
                            TelecomUseType tuse = pinfo.GetAttributeValue<RIMAttributeAttribute, TelecomUseType>(a => a.TelecomUse);                            
                        
                            TelecommunicationAddressUse usecode = TelecommunicationAddressUse.DIR;
                            if( tuse == TelecomUseType.HomeEmail || tuse == TelecomUseType.HomeFax || tuse == TelecomUseType.HomePhone )
                                usecode = TelecommunicationAddressUse.H;
                            else if( tuse == TelecomUseType.WorkEmail || tuse == TelecomUseType.WorkFax || tuse == TelecomUseType.WorkPhone )
                                usecode = TelecommunicationAddressUse.WP;
                            else if( tuse == TelecomUseType.Mobile )
                                usecode = TelecommunicationAddressUse.MC;

                            string prefix = "";
                            if( tuse == TelecomUseType.HomeEmail || tuse == TelecomUseType.WorkEmail )
                                prefix = "mailto:";
                            else if( tuse == TelecomUseType.HomePhone || tuse == TelecomUseType.WorkPhone || tuse == TelecomUseType.Mobile )
                                prefix = "tel:";
                            else if( tuse == TelecomUseType.HomeFax || tuse == TelecomUseType.WorkFax )
                                prefix = "fax:";

                            t.use = new TelecommunicationAddressUse[] { usecode };
                            t.value = prefix + v.ToString();
                            AssignValueOrArray(info, item, t);
                            break;

                        case RIMAttributeType.AdministrativeGenderCode:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else
                            {
                                // try coded enum
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;
                        case RIMAttributeType.EthnicGroupCode:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else
                            {
                                // try coded enum
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;
                        case RIMAttributeType.DischargeDispositionCode:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else
                            {
                                // try coded enum
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;

                        case RIMAttributeType.StatusCode:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else
                            {
                                // try coded enum
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                            }
                            break;
                        case RIMAttributeType.Title:
                            AssignValueOrArray(info, item, new ST() { Text = new string[] { v.ToString() } });
                            break;
                        case RIMAttributeType.EffectiveTime:
                            EffectiveTimeUseType use = pinfo.GetAttributeValue<RIMAttributeAttribute, EffectiveTimeUseType>(a => a.EffectiveTimeUse);
                            if( v is time_period )
                                AssignTimeInterval(info, item, use, v);
                            else
                                AssignTimeStamp(info, item, use, v);

                            break;
                        case RIMAttributeType.BirthTime:
                            EffectiveTimeUseType use2 = pinfo.GetAttributeValue<RIMAttributeAttribute, EffectiveTimeUseType>(a => a.EffectiveTimeUse);
                            AssignTimeStamp(info, item, use2, v);
                            break;
                        case RIMAttributeType.Text:
                            if( item is POCD_MT000040Section )
                                AssignValueOrArray(info, item, ReadStructDocText(v.ToString()));
                            else
                                AssignValueOrArray(info, item, new ST() { Text = new string[] { v.ToString() } });

                            break;
                        case RIMAttributeType.Value:
                            if (v is codeable)
                                AssignToCode(info, item, v);
                            else if (v is physical_quantity)
                                AssignValueOrArray(info, item, new PQ() { value = v.Value.ToString(), unit = v.Unit });
                            else
                            {
                                // coded enum member
                                dynamic cd = CodedEnumValue(info.PropertyType, v);
                                if (cd != null)
                                    AssignValueOrArray(info, item, cd);
                                else  // assume a string
                                    AssignValueOrArray(info, item, new ST() { Text = new string[] { v.ToString() } });
                            }


                            break;
                        case RIMAttributeType.Name:

                            if (v is name)
                            {
                                PN o = new PN();
                                o.use = new EntityNameUse[] { EntityNameUse.L };
                                o.family = new enfamily[] { new enfamily() { Text = new string[] { v.Family } } };
                                if (v.Given1 != null)
                                {
                                    if (v.Given2 != null)
                                        o.given = new engiven[] { new engiven() { Text = new string[] { v.Given1 } }, new engiven() { Text = new string[] { v.Given2 } } };
                                    else
                                        o.given = new engiven[] { new engiven() { Text = new string[] { v.Given1 } } };
                                }

                                AssignValueOrArray(info, item, o);
                            }
                            else
                                AssignValueOrArray(info, item, new ON() { use = new EntityNameUse[] { EntityNameUse.ORGB }, Text = new string[] { v.ToString() } });

                            break;

                        case RIMAttributeType.Time:
                            // applies to participation only - not the role
                            if (parentrelationship != null)
                            {
                                System.Reflection.PropertyInfo parentinfo = parentrelationship.GetType().GetProperty(rimattr.ToString().ToFirstLower());

                                if (v is time_period)
                                    AssignTimeInterval(parentinfo, parentrelationship, EffectiveTimeUseType.DateTimeZone, v);
                                else
                                    AssignTimeStamp(parentinfo, parentrelationship, EffectiveTimeUseType.DateTimeZone, v);
                            }
                            break;
                        case RIMAttributeType.SignatureCode:
                            if (parentrelationship != null)
                            {
                                System.Reflection.PropertyInfo parentinfo = parentrelationship.GetType().GetProperty(rimattr.ToString().ToFirstLower());

                                if (v is codeable)
                                    AssignToCode(parentinfo, parentrelationship, v);
                                else
                                {
                                    // try coded enum
                                    dynamic cd = CodedEnumValue(parentinfo.PropertyType, v);
                                    if (cd != null)
                                        AssignValueOrArray(parentinfo, parentrelationship, cd);
                                }
                            }
                            break;
                        case RIMAttributeType.HPII:
                            AssignExtId(item, v, "HPI-I", "1.2.36.1.2001.1003.0", null, "National Identifier", null);
                            break;
                        case RIMAttributeType.HPIO:
                            AssignExtId(item, v, "HPI-O", "1.2.36.1.2001.1003.0", null, "National Identifier", null);
                            break;
                        case RIMAttributeType.IHI:
                            AssignExtId(item, v, "IHI", "1.2.36.1.2001.1003.0", null, "National Identifier", null);
                            break;
                        case RIMAttributeType.MRN:
                            if (v is identifier)
                                AssignExtId(item, v.Extension, v.AuthorityName, null, v.Root , null, "MR");
                            else
                            {
                                string idroot2 = pinfo.GetAttributeValue<RIMAttributeAttribute, string>(a => a.IdRoot);
                                AssignExtId(item, v, "Medical Record Number", null, idroot2, null, "MR");
                            }
                            break;
                        case RIMAttributeType.PRN:
                            string idroot3 = pinfo.GetAttributeValue<RIMAttributeAttribute, string>(a => a.IdRoot);
                            AssignExtId(item, v, "Provider Number", null, idroot3, null, "PRN");
                            break;
                        case RIMAttributeType.LocalProviderID:
                            if (v is identifier)
                                AssignExtId(item, v.Extension, v.AuthorityName, null, v.Root, null, "EI");
                            else
                            {
                                string idroot4 = pinfo.GetAttributeValue<RIMAttributeAttribute, string>(a => a.IdRoot);
                                AssignExtId(item, v, "LocalProviderId", null, idroot4, null, "EI");
                            }
                            break;
                        case RIMAttributeType.Employer:
                            AssignEmployer(item, v);
                            break;

                        default:
                            break;
                       
                    }
                }

                // walk the relationships
                object childobj = pinfo.GetValue(data, null);
                if (childobj != null)
                {
                    // add named relationship
                    Type actreltype = pinfo.GetAttributeValue<RIMRelationshipAttribute, Type>(a => a.ElementType);
                    string actreltypecode = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.TypeCode);
                    string actrelfunctioncode = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.FunctionCode);
                    string actrelelementname = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.ElementName);
                    string actreltargetelementname = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.TargetElementName);
                                         
                    // todo: use Name to group values together under one instance

                    // check if pre-defined with child act
                    if (actreltargetelementname != null)
                    {
                        string containedclasscode = pinfo.GetAttributeValue<RIMContainerAct, string>(a => a.ClassCode);
                        if (containedclasscode == null)
                        {
                            // check if an array - otherwise just set
                            if (pinfo.PropertyType.AssemblyQualifiedName.Contains("System.Collections.Generic.List"))
                            {
                                dynamic list = childobj;
                                foreach (object childobjitem in list)
                                {
                                    // create relation and add instance
                                    if (actreltype != null)
                                    {
                                        dynamic relation = Activator.CreateInstance(actreltype);

                                        if (actreltypecode != null)
                                            AssignToStringOrEnum("typeCode", relation, actreltypecode);

                                        if (actrelfunctioncode != null)
                                            AssignToCode("functionCode", relation, actrelfunctioncode, null, null, null);

                                        AssignValueOrArray(item.GetType().GetProperty(actrelelementname), item, relation);

                                        System.Reflection.PropertyInfo dest = relation.GetType().GetProperty(actreltargetelementname);

                                        AssignValueOrArray(dest, relation, RIMActFactory(relation, childobjitem));
                                    }
                                    else
                                    {
                                        // just a reference
                                        System.Reflection.PropertyInfo dest = item.GetType().GetProperty(actreltargetelementname);
                                        AssignValueOrArray(dest, item, RIMActFactory(null, childobjitem));
                                    }

                                }

                            }
                            else
                            {
                                // create relation and add instance
                                if (actreltype != null)
                                {
                                    dynamic relation = Activator.CreateInstance(actreltype);

                                    if (actreltypecode != null)
                                        AssignToStringOrEnum("typeCode", relation, actreltypecode);

                                    if (actrelfunctioncode != null)
                                        AssignToCode("functionCode", relation, actrelfunctioncode, null, null, null);

                                    AssignValueOrArray(item.GetType().GetProperty(actrelelementname), item, relation);

                                    System.Reflection.PropertyInfo dest = relation.GetType().GetProperty(actreltargetelementname);

                                    AssignValueOrArray(dest, relation, RIMActFactory(relation, childobj));
                                }
                                else
                                {
                                    // just a reference
                                    System.Reflection.PropertyInfo dest = item.GetType().GetProperty(actreltargetelementname);
                                    AssignValueOrArray(dest, item, RIMActFactory(null, childobj));
                                }
                            }
                        }
                        else
                        {
                            // construct container - only handles cardinality 1 at the moment
                            // only handles observation/act really...

                            Type containedtype = pinfo.GetAttributeValue<RIMContainerAct, Type>(a => a.ElementType);
                            string containedmood = pinfo.GetAttributeValue<RIMContainerAct, string>(a => a.MoodCode);
                            bool autoid = pinfo.GetAttributeValue<RIMContainerAct, bool>(a => a.AutoId);
                            string ID = pinfo.GetAttributeValue<RIMContainerAct, string>(a => a.ID);

                            dynamic relation = Activator.CreateInstance(actreltype);
                            AssignToStringOrEnum("typeCode", relation, actreltypecode);
                        
                            dynamic container = Activator.CreateInstance(containedtype);
                            AssignValueOrArray(output.GetType().GetProperty(actrelelementname), output, relation);

                            AssignToStringOrEnum("classCode", container, containedclasscode);

                            if (output.GetType().GetProperty("moodCode") != null)
                                AssignToStringOrEnum("moodCode", container, containedmood);

                            // assign guid id automatically 
                            if (autoid)
                                AssignValueOrArray(container.GetType().GetProperty("id"), container, new II() { root = System.Guid.NewGuid().ToString() });

                            if( ID != null )
                                AssignValueOrArray(container.GetType().GetProperty("ID"), container, ID);

                            // inject act
                            System.Reflection.PropertyInfo dest = relation.GetType().GetProperty(actreltargetelementname);
                            AssignValueOrArray(dest, relation, container);

                            // set act.code
                            string ccode = pinfo.GetAttributeValue<RIMContainerActCode, string>(a => a.Code);
                            string ccodeSystem = pinfo.GetAttributeValue<RIMContainerActCode, string>(a => a.CodeSystem);
                            if (ccode != null && ccodeSystem != null)
                            {
                                string cdisplayName = pinfo.GetAttributeValue<RIMContainerActCode, string>(a => a.DisplayName);
                                string ccodeSystemName = pinfo.GetAttributeValue<RIMContainerActCode, string>(a => a.CodeSystemName);
                                AssignToCode(container, ccode, ccodeSystem, ccodeSystemName, cdisplayName);
                            }

                            // if type is act->text, observation->value
                            dynamic v = pinfo.GetValue(data, null);
                            if (container is POCD_MT000040Act)
                                AssignToText(container, v);
                            if (container is POCD_MT000040Supply)
                                AssignToText(container, v);
                            else if (container is POCD_MT000040Observation)
                                AssignToValue(container, v);
                            else if (container is POCD_MT000040ObservationMedia)
                            {
                                string ext = System.IO.Path.GetExtension(v);

                                string mtype = string.Empty;
                                if (ext == ".jpg")
                                    mtype = "image/jpg";
                                else if (ext == ".png")
                                    mtype = "image/png";
                                else if (ext == ".gif")
                                    mtype = "image/gif";
                                else if (ext == ".bmp")
                                    mtype = "image/bmp";

                                string fname = System.IO.Path.GetFileName(v);

                                byte[] integrityCheckData = CalculateSHA1(System.IO.File.ReadAllBytes(v));
                                
                                (container as POCD_MT000040ObservationMedia).value = new ED() { reference = new TEL() { value = fname }, integrityCheck=integrityCheckData, integrityCheckAlgorithm = IntegrityCheckAlgorithm.SHA1, mediaType = mtype };
                            }
                        }
                    }
                        
                    
                }
            }


            return output;
        }

        private static byte[] CalculateSHA1(byte[] content)
        {
            var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            return sha1CryptoServiceProvider.ComputeHash(content);
        }

        static protected Nehta.HL7.CDA.StrucDocText ReadStructDocText(string input)
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Nehta.HL7.CDA.StrucDocText));

            //Nehta.HL7.CDA.StrucDocText test = new StrucDocText();
            //test.paragraph = new StrucDocParagraph[] { new StrucDocParagraph() { Text = new string[] { "TEST", "MORE" } } };
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //ser.Serialize(sw, test);
            //string xml = sw.GetStringBuilder().ToString();

            System.IO.StringReader sr = new System.IO.StringReader("<StrucDoc.Text mediaType=\"text/x-hl7-text+xml\">" + input + "</StrucDoc.Text>");
            Nehta.HL7.CDA.StrucDocText output = ser.Deserialize(sr) as Nehta.HL7.CDA.StrucDocText;
            return output;
        }

        static protected void AssignToStringOrEnum(string pname, dynamic output, string code) 
        {
            
            // set RIM vocab
            try
            {
                if (output.GetType().GetProperty(pname).PropertyType == typeof(string))
                    output.GetType().GetProperty(pname).SetValue(output, code, null);
                else
                {
                    dynamic v = Enum.Parse(output.GetType().GetProperty(pname).PropertyType, code);
                    output.GetType().GetProperty(pname).SetValue(output, v, null);

                    if (output.GetType().GetProperty(pname + "Specified") != null)
                        output.GetType().GetProperty(pname + "Specified").SetValue(output, true, null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ex AssignToStringOrEnum: " + pname + "(" + output.GetType().GetProperty(pname).PropertyType.ToString() + ") = " + code, ex);
            }
        }

        static protected void AssignToText(object destobj, dynamic v)
        {
            System.Reflection.PropertyInfo info = destobj.GetType().GetProperty("text");
            AssignValueOrArray(info, destobj, new ST() { Text = new string[] { v.ToString() } });
        }

        static protected TEL TelecomValue(string value, TelecomUseType tuse)
        {
            TEL t = new TEL();
           
            TelecommunicationAddressUse usecode = TelecommunicationAddressUse.DIR;
            if (tuse == TelecomUseType.HomeEmail || tuse == TelecomUseType.HomeFax || tuse == TelecomUseType.HomePhone)
                usecode = TelecommunicationAddressUse.H;
            else if (tuse == TelecomUseType.WorkEmail || tuse == TelecomUseType.WorkFax || tuse == TelecomUseType.WorkPhone)
                usecode = TelecommunicationAddressUse.WP;
            else if (tuse == TelecomUseType.Mobile)
                usecode = TelecommunicationAddressUse.MC;

            string prefix = "";
            if (tuse == TelecomUseType.HomeEmail || tuse == TelecomUseType.WorkEmail)
                prefix = "mailto:";
            else if (tuse == TelecomUseType.HomePhone || tuse == TelecomUseType.WorkPhone || tuse == TelecomUseType.Mobile)
                prefix = "tel:";
            else if (tuse == TelecomUseType.HomeFax || tuse == TelecomUseType.WorkFax)
                prefix = "fax:";

            t.use = new TelecommunicationAddressUse[] { usecode };
            t.value = prefix + value.ToString();

            return t;
        }

        static protected AD AddrValue(address a, AddressUseType u)
        {
            AD output = new AD();

            if (a.AddressLine1 != null)
            {
                if (a.AddressLine2 != null)
                {
                    if (a.AddressLine3 != null)
                    {
                        if (a.AddressLine4 != null)
                        {
                            output.streetAddressLine = new adxpstreetAddressLine[] { 
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine1 } }, 
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine2 } },
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine3 } },
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine4 } } };
                        }
                        else
                        {
                            output.streetAddressLine = new adxpstreetAddressLine[] { 
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine1 } }, 
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine2 } },
                            new adxpstreetAddressLine() { Text= new string[] { a.AddressLine3 } }};
                        }
                    }
                    else
                    {
                        output.streetAddressLine = new adxpstreetAddressLine[] { 
                        new adxpstreetAddressLine() { Text= new string[] { a.AddressLine1 } }, 
                        new adxpstreetAddressLine() { Text= new string[] { a.AddressLine2 } }};
                    }
                }
                else
                {
                    output.streetAddressLine = new adxpstreetAddressLine[] { 
                        new adxpstreetAddressLine() { Text= new string[] { a.AddressLine1 } } };
                }
            }

            if (a.Postcode != null)
                output.postalCode = new adxppostalCode[] { new adxppostalCode() { Text = new string[] { a.Postcode } } };

            if (a.Suburb != null)
                output.city = new adxpcity[] { new adxpcity() { Text = new string[] { a.Suburb } } };

            if (a.State != null)
                output.state = new adxpstate[] { new adxpstate() { Text = new string[] { a.State } } };

            if(a.Country != null)
                output.country = new adxpcountry[] { new adxpcountry() { Text = new string[] { a.Country } } };

            if (u == AddressUseType.Home)
                output.use = new PostalAddressUse[] { PostalAddressUse.H };
            else if( u == AddressUseType.Workplace )
                output.use = new PostalAddressUse[] { PostalAddressUse.WP };
            else if (u == AddressUseType.Temporary)
                output.use = new PostalAddressUse[] { PostalAddressUse.TMP };
            else if (u == AddressUseType.Postal)
                output.use = new PostalAddressUse[] { PostalAddressUse.PST };
            

            return output;
        }



        static protected void AssignToValue(object item, dynamic v)
        {
            System.Reflection.PropertyInfo info = item.GetType().GetProperty("value");

            if (v is codeable)
                AssignToCode(info, item, v);
            else if (v is physical_quantity)
                AssignValueOrArray(info, item, new PQ() { value = v.Value.ToString(), unit = v.Unit });
            else
            {
                // coded enum member
                dynamic cd = CodedEnumValue(info.PropertyType, v);
                if (cd != null)
                    AssignValueOrArray(info, item, cd);
                else  // assume a string
                    AssignValueOrArray(info, item, new ST() { Text = new string[] { v.ToString() } });
            }
        }

        static protected void AssignToCode(System.Reflection.PropertyInfo pinfo, object destobj, codeable v)
        {
            dynamic output = null;

            if (pinfo.PropertyType == typeof(ANY) || pinfo.PropertyType == typeof(ANY[]))
                output = new CD();
            else
                output = Activator.CreateInstance(pinfo.PropertyType);
            
            if( v.Code != null )
            {
                output.code = v.Code;
                output.codeSystem = v.CodeSystem;
                output.codeSystemName = v.CodeSystemName;
                output.displayName = v.DisplayName;
            }
            
            if( v.OriginalText != null )
                output.originalText = new ED() { Text = new string[] { v.OriginalText } };

            if (v.NullFlavor != null)
            {
                Nehta.HL7.CDA.NullFlavor nf = NullFlavor.NI;
                if (Enum.TryParse<Nehta.HL7.CDA.NullFlavor>(v.NullFlavor, out nf))
                {
                    output.nullFlavor = nf;
                    output.nullFlavorSpecified = true;
                }
            }

            AssignValueOrArray(pinfo, destobj, output);
        }

        static protected void AssignToCode(string attrname, dynamic destobj, string code, string codeSystem, string codeSystemName, string displayName)
        {
            System.Reflection.PropertyInfo info = destobj.GetType().GetProperty(attrname);
            if (info != null)
            {
                dynamic ocode = Activator.CreateInstance(info.PropertyType);
                ocode.code = code;
                ocode.codeSystem = codeSystem;
                ocode.displayName = displayName;
                ocode.codeSystemName = codeSystemName;
                info.SetValue(destobj, ocode, null);
            }

        }

        static protected void AssignToCode(dynamic destobj, string code, string codeSystem, string codeSystemName, string displayName)
        {
            System.Reflection.PropertyInfo info = destobj.GetType().GetProperty("code");
            if (info != null)
            {
                dynamic ocode = Activator.CreateInstance(info.PropertyType);
                ocode.code = code;
                ocode.codeSystem = codeSystem;
                ocode.displayName = displayName;
                ocode.codeSystemName = codeSystemName;
                destobj.code = ocode;
            }

         }
            

        static protected dynamic CodedEnumValue(Type desttype, dynamic v)
        {
            if ((v as Enum) == null)
                return null;

            string gcode = (v as Enum).GetAttributeValue<CodedAttribute, string>(a => a.Code);
            if (gcode == null)
                return null;

           dynamic cd = (desttype == typeof(ANY) || desttype == typeof(ANY[])) ? new CD() : Activator.CreateInstance(desttype);
           cd.code = gcode;
           cd.codeSystem = (v as Enum).GetAttributeValue<CodedAttribute, string>(a => a.CodeSystem);
           cd.displayName = (v as Enum).GetAttributeValue<CodedAttribute, string>(a => a.DisplayName);
           cd.codeSystemName = (v as Enum).GetAttributeValue<CodedAttribute, string>(a => a.CodeSystemName);

           return cd;
        }

        static protected void AssignEmployer(dynamic item, employer e)
        {
            Employment o = new Employment();
            if( e.JobRole != null )
                o.code = new CE() { code = e.JobRole.Code, codeSystem = e.JobRole.CodeSystem, codeSystemName = e.JobRole.CodeSystemName, displayName = e.JobRole.DisplayName };

            o.employerOrganization = new POCD_MT000040Organization();

            if (e.DepartmentName != null)
                o.employerOrganization.name = new ON[] { new ON() { use = new EntityNameUse[] { EntityNameUse.ORGU }, Text = new string[] { e.DepartmentName } } };

            if(  e.OrganizationName != null )
            {
                o.employerOrganization.asOrganizationPartOf = new POCD_MT000040OrganizationPartOf()
                {
                    wholeOrganization = new POCD_MT000040Organization()
                    {
                        name = new ON[] { new ON() { use = new EntityNameUse[] { EntityNameUse.ORGB }, Text = new string[] { e.OrganizationName } } },
                    }
                };

                if (e.HPIO != null)
                {
                    o.employerOrganization.asOrganizationPartOf.wholeOrganization.asEntityIdentifier = new EntityIdentifier[] { 
                        new EntityIdentifier() {
                            classCode = EntityClass.IDENT,
                            id = new II() { assigningAuthorityName = "HPI-O", root="1.2.36.1.2001.1003.0." + e.HPIO },
                            assigningGeographicArea = new GeographicArea()
                            {
                                classCode = EntityClass.PLC, 
                                name=new ST() { Text=new string[] { "National Identifier" } },
                            }
                        }
                    };
                };
            }

            if (e.Address != null)
                o.employerOrganization.addr = new AD[] { AddrValue(e.Address, AddressUseType.Workplace) };

            List<TEL> telecoms = new List<TEL>();
            if (e.WorkEmail != null)
                telecoms.Add(TelecomValue(e.WorkEmail, TelecomUseType.WorkEmail));
            if (e.WorkPhone != null)
                telecoms.Add(TelecomValue(e.WorkPhone, TelecomUseType.WorkPhone));
            if (e.WorkFax != null)
                telecoms.Add(TelecomValue(e.WorkFax, TelecomUseType.WorkFax));


            o.employerOrganization.telecom = telecoms.ToArray();


            item.asEmployment = o;
        }

        static protected void AssignTimeInterval(System.Reflection.PropertyInfo info, object item, EffectiveTimeUseType use, dynamic v)
        {
            string format = "yyyyMMddHHmmss";
            if (use == EffectiveTimeUseType.DateOnly)
                format = "yyyyMMdd";
            if (use == EffectiveTimeUseType.DateTimeHHMMZone)
                format = "yyyyMMddHHmm";
            else if (use == EffectiveTimeUseType.MonthDateOnly)
                format = "yyyyMM";

            List<QTY> parts = new List<QTY>();
            List<ItemsChoiceType3> names = new List<ItemsChoiceType3>();
            if (v.From != null)
            {
                string lowts = v.From.ToString(format);

                if (use == EffectiveTimeUseType.DateTimeZone)
                {
                    TimeSpan offset = timezoneinfo.GetUtcOffset(v.From);
                    lowts += ((offset.TotalHours < 0) ? "-" : "+") + offset.ToString("hhmm");
                }

                parts.Add(new IVXB_TS() { value = lowts, inclusive = v.FromInclusive });
                names.Add(ItemsChoiceType3.low);
            }

            if (v.To != null)
            {
                string hights = v.To.ToString(format);

                if (use == EffectiveTimeUseType.DateTimeZone)
                {
                    TimeSpan offset = timezoneinfo.GetUtcOffset(v.To);
                    hights += ((offset.TotalHours < 0) ? "-" : "+") + offset.ToString("hhmm");
                }

                parts.Add(new IVXB_TS() { value = hights, inclusive = v.ToInclusive });
                names.Add(ItemsChoiceType3.high);
            }

            IVL_TS output = new IVL_TS();
            output.Items = parts.ToArray();
            output.ItemsElementName = names.ToArray();            

            AssignValueOrArray(info, item, output);
        }
       
        static protected void AssignTimeStamp(System.Reflection.PropertyInfo info, object item, EffectiveTimeUseType use, dynamic v )
        {
            string format = "yyyyMMddHHmmss";
            if (use == EffectiveTimeUseType.DateOnly)
                format = "yyyyMMdd";
            if (use == EffectiveTimeUseType.DateTimeHHMMZone)
                format = "yyyyMMddHHmm";
     
            string ts = v.ToString(format);

            if (use == EffectiveTimeUseType.DateTimeZone)
            {
                TimeSpan offset = timezoneinfo.GetUtcOffset(v);
                ts += ((offset.TotalHours < 0) ? "-" : "+") + offset.ToString("hhmm");
            }

            if( info.PropertyType == typeof(IVL_TS))
                AssignValueOrArray(info, item, new IVL_TS() { value = ts });
            else if(info.PropertyType == typeof(TS))
                AssignValueOrArray(info, item, new TS() { value = ts });

        }

        static protected void AssignExtId(object item, dynamic v, string authority, string rootprefix, string idroot, string geographicarea, string idcode)
        {
            EntityIdentifier extid = new EntityIdentifier();
            extid.classCode = EntityClass.IDENT;

            if (idroot == null)
                extid.id = new II() { assigningAuthorityName = (authority == null) ? null : authority, root = (rootprefix != null) ? rootprefix + "." + v.ToString() : v.ToString() };
            else
                extid.id = new II() { assigningAuthorityName = (authority == null) ? null : authority, root = idroot, extension = v.ToString() };

            if( geographicarea != null )
                extid.assigningGeographicArea = new GeographicArea() { classCode=EntityClass.PLC, name=new ST() { Text=new string[] { geographicarea } } };
            
            if( idcode != null )
                extid.code = new CE() { code=idcode, codeSystem="2.16.840.1.113883.12.203", codeSystemName="Identifier Type (HL7)" };
            
            AssignValueOrArray( item.GetType().GetProperty("asEntityIdentifier"), item, extid);
        }

        static protected void AssignValueOrArray(System.Reflection.PropertyInfo pinfo, object destobj, object destvalue)
        {
            if (pinfo.PropertyType.IsArray)
            {
                object current = pinfo.GetValue(destobj, null);
                dynamic destlist = (current == null)?Activator.CreateInstance(typeof(List<>).MakeGenericType(destvalue.GetType())): Activator.CreateInstance(typeof(List<>).MakeGenericType(destvalue.GetType()), new [] {current} );
                System.Reflection.MethodInfo mi = destlist.GetType().GetMethod("Add");
                mi.Invoke(destlist, new[] { destvalue });
                pinfo.SetValue(destobj, destlist.ToArray(), null);
            }
            else
                pinfo.SetValue(destobj, destvalue, null);

        }
    }

}
