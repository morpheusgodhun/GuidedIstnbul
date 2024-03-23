using Dto.ApiWebDtos.GeneralDtos;

namespace Core.StaticValues
{
    //bu ayarlar sitede kullanılacak 4-5 farklı tipteki rol yapısını temsil eder
    public class ConstantRoles
    {
        public static List<SelectListOptionDto> SpecialRoleTemplatesGuid = new()
        {
            new() { OptionID = new Guid("89F6763A-0852-4E87-BA6E-B6A51EF00C6A"), Option = "Member" },
            new() { OptionID = new Guid("415CB898-18DD-4FAE-93FB-FA7A43A61A12"), Option = "Admin" },
            new() { OptionID = new Guid("96054655-597A-41D7-98A6-88F60BB25B6E"), Option = "Customer" },
            new() { OptionID = new Guid("61AA5DAA-0E5F-4FC9-854E-34F1B19FB08D"), Option = "Guide" },
            new() { OptionID = new Guid("CEB3F42A-4CD8-4A0B-917F-E6228D09B3BA"), Option = "Agent" },
            new() { OptionID = new Guid("64536B6D-E77E-4EFE-AD19-10A0793476E0"), Option = "AgentAccounter" },
            new() { OptionID = new Guid("6E26F0C8-6B2E-4CBD-B7CD-280747F7066B"), Option = "AgentDriver" },
            new() { OptionID = new Guid("229E26BA-4EEB-4B07-8D90-B7669D7CE195"), Option = "AgentGuide" },
            new() { OptionID = new Guid("2C84D482-2E75-435C-89F8-3451A5562C8B"), Option = "Driver" },
        };

        public static List<SelectListOptionDto> SpecialRolesGuid = new()
        {
            new() { OptionID = new Guid("57458BFF-DECE-4A0F-90F4-0921454B78CA"), Option = "Member" },
            new() { OptionID = new Guid("01B01966-FBC7-440E-B500-B26E7C220F46"), Option = "Admin" },
            new() { OptionID = new Guid("79894F67-568E-4061-8F2B-4727AFBF0B86"), Option = "Customer" },
            new() { OptionID = new Guid("CEE76EA5-C1C9-4627-89E7-699B69088246"), Option = "Guide" },
            new() { OptionID = new Guid("97CC9BC0-63E1-487F-94BB-6DF9EC39C9C8"), Option = "Agent" },
            new() { OptionID = new Guid("43404319-1A6E-4B1C-9EF4-7CDE67BD5AC6"), Option = "AgentAccounter" },
            new() { OptionID = new Guid("EA274801-2F76-41C1-BD86-ABB298255F83"), Option = "AgentDriver" },
            new() { OptionID = new Guid("763B9849-221D-4CD4-BD25-E2336D4EA744"), Option = "AgentGuide" },
            new() { OptionID = new Guid("4AC6F423-7712-41F2-A868-9AEEEF805BCE"), Option = "Driver" },
        };


        public static SelectListOptionDto GetRole(int no)
        {
            return SpecialRolesGuid[no - 1];
        }

        public static SelectListOptionDto GetRoleTemplate(int no)
        {
            return SpecialRoleTemplatesGuid[no - 1];
        }

        public enum No
        {
            Member = 1,
            Admin = 2,
            Customer = 3,
            Guide = 4,
            Agent = 5,
            AgentAccounter = 6,
            AgentDriver = 7,
            AgentGuide = 8,
            Driver = 9,
        }


        public static Guid[] AgentRoleTemplates = new[] {
            GetRoleTemplate((int)No.Agent).OptionID,
            GetRoleTemplate((int)No.AgentAccounter).OptionID,
            GetRoleTemplate((int)No.AgentDriver).OptionID,
            GetRoleTemplate((int)No.AgentGuide).OptionID
        };

    }
}
/*
 
ROLEtemplate

Id	Name
89F6763A-0852-4E87-BA6E-B6A51EF00C6A	Member
415CB898-18DD-4FAE-93FB-FA7A43A61A12	Admin

96054655-597A-41D7-98A6-88F60BB25B6E	Customer
61AA5DAA-0E5F-4FC9-854E-34F1B19FB08D	Guide
CEB3F42A-4CD8-4A0B-917F-E6228D09B3BA	Agent


64536B6D-E77E-4EFE-AD19-10A0793476E0	AgentAccounter
6E26F0C8-6B2E-4CBD-B7CD-280747F7066B	AgentDriver
229E26BA-4EEB-4B07-8D90-B7669D7CE195	AgentGuide


Role

Id	Name
57458BFF-DECE-4A0F-90F4-0921454B78CA	Member
01B01966-FBC7-440E-B500-B26E7C220F46	Admin

79894F67-568E-4061-8F2B-4727AFBF0B86	Customer
CEE76EA5-C1C9-4627-89E7-699B69088246	Guide
97CC9BC0-63E1-487F-94BB-6DF9EC39C9C8	Agent


43404319-1A6E-4B1C-9EF4-7CDE67BD5AC6	AgentAccounter
EA274801-2F76-41C1-BD86-ABB298255F83	AgentDriver
763B9849-221D-4CD4-BD25-E2336D4EA744	AgentGuide

 
 */