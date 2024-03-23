using Core.Entities;
using Core.IRepository;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticValues;

namespace Service.Services
{
    public class Many_Role_RoleTemplateService : GenericService<Many_Role_RoleTemplate>, IMany_Role_RoleTemplateService
    {
        readonly IRolePermissionRepository _rolePermissionRepository;
        public Many_Role_RoleTemplateService(IGenericRepository<Many_Role_RoleTemplate> repository, IUnitOfWork unitOfWork, IRolePermissionRepository roleRepository) : base(repository, unitOfWork)
        {
            _rolePermissionRepository = roleRepository;
        }

        public void SaveAdminRolePermissions(List<string> controllerNames)
        {
            var controllerStr = "Controller";
            var managementStr = "Management";
            List<RolePermission> rolePermissionsToAdd = new();

            Guid adminRoleTemplate = ConstantRoles.GetRoleTemplate((int)ConstantRoles.No.Admin).OptionID;
            //admin bağımsız all role permissionlar
            var existingRolePermissions = _rolePermissionRepository.GetAll();
            List<Many_Role_RoleTemplate> existingAdminPermissionsOnDb = _repository.GetAll().Where(x => x.RoleTemplateId == adminRoleTemplate).ToList();

            ///panelden gelen controllerları db ye ekleyen loop
            foreach (string controllerName in controllerNames)
            {
                if (controllerName.EndsWith(controllerStr))
                {
                    string rolePermissionName = $"{controllerName[..^controllerStr.Length]}{managementStr}";

                    if (existingRolePermissions.Where(x => x.Name == rolePermissionName).FirstOrDefault() is null)
                        _rolePermissionRepository.Add(new(rolePermissionName));
                }
            }
            ///burdan sonra admin rollerini dbye ekleyecem
            _unitOfWork.saveChanges();

            ///admin rollerini db ye ekleyen loop

            foreach (var rolePermission in existingRolePermissions)
            {
                var existingPermission = existingAdminPermissionsOnDb.Where(x => x.RolePermissionId == rolePermission.Id && x.RolePermission.Name.EndsWith(managementStr)).FirstOrDefault();
                if (existingPermission is null)
                {
                    Many_Role_RoleTemplate entity = new()
                    {
                        RolePermissionId = rolePermission.Id,
                        RoleTemplateId = adminRoleTemplate,
                    };
                    _repository.Add(entity);
                }
            }
            _unitOfWork.saveChanges();

            //_unitOfWork.saveChanges();
        }
    }
}

/*
 * 
 * 
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('48EEDA54-1626-4D9A-8730-00E7AC8D81BF','BDDA3CF1-2AFA-41A7-B4B6-85D1977A4BF2','77D5361A-FF4D-4030-B3EA-ECF6DDFC8A37','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('9A30372D-CEB3-432F-AB4B-0209D141B3AB','763B9849-221D-4CD4-BD25-E2336D4EA744','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('5D41FC11-1095-487C-A976-0467C5D1E84E','97CC9BC0-63E1-487F-94BB-6DF9EC39C9C8','CEB3F42A-4CD8-4A0B-917F-E6228D09B3BA','2023-11-30',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('0F24DA6F-1478-464E-AC75-184A61B1E75C','53871E30-CD5A-4E17-91ED-FCF8027C3299','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('142B084A-6F7B-402C-9A48-1BE80E5B22BE','9EA9B290-A91B-4E24-8E23-92715FCEF99D','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('DE91C2F7-FDF4-4019-B9CF-27DA87908717','094AB07B-26DB-456A-BEF2-FA11A24EACCE','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('D24EEE33-C2FE-42E0-866D-2B152EDC586D','79894F67-568E-4061-8F2B-4727AFBF0B86','96054655-597A-41D7-98A6-88F60BB25B6E','2023-11-30',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('7A1F07A0-E326-40F8-9115-2D374A7B4DA9','763B9849-221D-4CD4-BD25-E2336D4EA744','229E26BA-4EEB-4B07-8D90-B7669D7CE195','2023-11-30',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('0D2B699D-61D1-4483-8ED0-3154461669A4','43404319-1A6E-4B1C-9EF4-7CDE67BD5AC6','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('72BD5F7B-BB7A-4BE9-B0AE-319B9393F492','37FBCE88-DBAA-4D5E-A6D2-E1B7B22EF1C7','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('6381BFD6-7590-47C9-B59B-35B565FC5465','68F82810-639B-4927-8052-A321DC21E5D3','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('6DE8D327-153B-4066-B268-36F1765288E4','CEE76EA5-C1C9-4627-89E7-699B69088246','61AA5DAA-0E5F-4FC9-854E-34F1B19FB08D','2023-11-30',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('5A8AA61B-CC31-4355-BB95-3C756A758A7B','6A6453A9-1B98-473A-85BD-43A0129E731E','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('E121E335-556C-411B-A052-3C949EFBBEC8','D08D1655-9C58-4015-8EAB-54729CC157FD','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('773B2556-E531-461B-822D-3D4E23A63AF6','95486040-C8BE-4F6C-8CD4-BB18AED50958','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('640B185A-0F2F-428B-AD40-45A3E767534F','CCEDABEB-76FE-4B81-B3CB-4591DAA153A2','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('288A9433-FA68-4DBB-96B3-533A4DA48CF0','986FF84C-9EB9-4065-A8A3-B4286CB9A98E','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('A344E19A-5240-468F-ABE3-673B55D0D396','18659070-C64D-41AD-88F6-D23268C269C5','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('22BC37D7-C2BA-4F15-9C8C-697DF91C245E','7617105C-C670-421D-9504-0DB4291DC23E','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('90DFD13B-7A01-4EF0-AF49-6E342F0ED63F','D306444E-261E-4A38-B32F-C137FCBDE9ED','77D5361A-FF4D-4030-B3EA-ECF6DDFC8A37','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('EAB188D9-8CEF-49F1-B647-721044868B4F','BDDA3CF1-2AFA-41A7-B4B6-85D1977A4BF2','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('0DE03757-9536-4DFC-8DC5-73DF893B5BFA','D5AE0B57-8F02-4268-9F76-E633E5676F21','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('95E35CCC-A199-4D23-8F80-7983D453B572','C2CDF474-E43F-47A3-81A8-145599B554A5','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('9FEB634A-8ADD-462D-B87E-7AC71B510215','1C99C0F7-46DB-4F9A-9A9D-0A8AAA64E367','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('EDE73D94-C62F-4997-B506-7BDCA6B22570','D306444E-261E-4A38-B32F-C137FCBDE9ED','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('1EEDF435-9D85-4E60-A858-8195C76E1835','0B138A9B-C0C3-4124-A566-38AB5360556C','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('245A3499-1AF7-4790-A719-82AE5E3484D1','3FA0B14B-A7D9-40B7-AFBF-2843FF1C9415','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('50808091-E4EE-4041-AB87-8FAC9BFD99A1','19A86E28-3467-4B4E-ADD2-225F638A2CE3','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('CC3852DF-0295-4645-AEC2-962DE37E826B','9D4CD8CA-4235-4283-9FC5-2DDF05408BAE','77D5361A-FF4D-4030-B3EA-ECF6DDFC8A37','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('67C2FB68-660C-4280-9F40-97B86D26954F','D1BAD32F-1243-42EF-A897-6833744691B2','DD5554C0-4D8F-4728-9EBF-4A4C445CF314','2023-09-29',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('FEFB2B06-2CA6-4085-AC46-9845E6B7EFFE','B4779E75-87C6-45A6-B477-CB27CB644F55','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('A7D945C8-8FDA-4ACD-A316-98C6BCF00C18','71404E68-5167-4201-B231-593A836B73A3','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('33D0DE1E-5012-49B8-B6A5-9CDE81210224','AA40BCE2-CB51-4D84-A5BC-89649BFF307F','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('045C0AAA-0F8D-4355-8B11-9E30FEE73190','7A68A1F2-4BD2-4A72-88C5-D7C6813D65C7','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('AD01A326-ECDC-42A4-B7A3-A0784EE61B99','EA274801-2F76-41C1-BD86-ABB298255F83','6E26F0C8-6B2E-4CBD-B7CD-280747F7066B','2023-11-30',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('665FE4BE-E12E-4725-8FAF-A32AB7031762','C158E2C8-9C2D-4EE0-9C1D-E34D366A759C','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('449B055C-C1E1-4ABE-BDF2-A36A4686CAF7','01B01966-FBC7-440E-B500-B26E7C220F46','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2007-07-07',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('331660AB-A862-48DC-8A69-A394EA4C8705','F1314CEE-8B18-4741-A8C7-EA439905151D','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('C516ECD4-BCCC-4BC2-A297-A45954347B19','50410BD2-849D-456C-A700-7DB1E9FECC37','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('36AC35B2-DA58-4600-9290-AB8EDF8112A9','C8F309CE-2DD9-49D3-B08B-5C599636A30F','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('19242B9E-53D9-4CBD-8FA7-B2FE0E62519B','B08EED73-45C5-49F2-B0B6-158B16079BD0','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('712D6960-6D0B-4CAB-B1C4-B65CEA419D80','9D4CD8CA-4235-4283-9FC5-2DDF05408BAE','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('158261B2-DF29-4F15-A685-B679A6C31852','9D61790A-DD39-4023-BBB9-637FDE053533','DD5554C0-4D8F-4728-9EBF-4A4C445CF314','2023-09-29',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('7052146D-7C99-467C-AC03-B82541E15257','30D1572A-1934-4427-A2B9-25983AC605A6','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('5A234ED4-8656-4A2E-8A64-BE3156D12DFB','4AC6F423-7712-41F2-A868-9AEEEF805BCE','2C84D482-2E75-435C-89F8-3451A5562C8B','2023-12-28',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('ECE680A7-FD7A-4AC3-BA52-BED53BDA1502','D1BAD32F-1243-42EF-A897-6833744691B2','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('B11BBDBA-B6C2-4AE8-A43C-CA8B2D8252B4','9D61790A-DD39-4023-BBB9-637FDE053533','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('98EE6B03-B6C3-47D1-8937-D3247CF3734E','1E6CFA88-6560-47B1-BFA7-BC2FE9F38725','77D5361A-FF4D-4030-B3EA-ECF6DDFC8A37','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('F9ACD9DA-B76A-4ED0-9323-D59653D9E9B5','2E5B434F-F058-4833-93E4-3221A55A2FF4','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('3FA52126-CC1E-4907-8F9C-D8840E51EA33','5CC8035A-71B9-4E8A-B3E2-79D902A66836','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('68CD7316-A853-4032-9D82-DA351BE0D39C','6CEF35F1-3CD0-4088-A29E-0F6BAD075AC6','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('28AE3D3C-2EEA-4B90-BE83-DAFD7B92F227','1E6CFA88-6560-47B1-BFA7-BC2FE9F38725','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('95E15F29-1C99-4E89-8DBD-DBB99424724B','62B364F4-B722-400E-A747-FE33FD4025B6','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('34971A1F-0954-4B83-A01D-DD3D582764C8','C64B2AF7-E852-4EF8-BB58-E9B85D3809AC','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('9308B05C-18D2-473D-A97B-DE15E287A6D7','FB5D5277-9645-4169-B18E-AD94D47FD9A2','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('4A41E970-6F9D-4DE0-81C9-DE9697C8164F','4CDD773A-AF1B-49F8-87DA-6B4B0DBA949C','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('C7F307C0-749E-4F6D-ADC6-E67EF5E9506A','AA26CA5F-47FC-41F0-8DAC-999041EA811F','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('860B48F0-E507-4F83-86A0-E70DDD152991','F952DC37-C2CC-4842-B32F-27E2A0682EF6','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('5C73E242-C721-42B8-BA56-F6F6ED801D25','71C0207D-7A39-4A1A-A886-53362C9ED6D2','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('1291A8BC-17A2-4584-A007-FB8D1F1813BE','8043A118-D288-4ACC-8098-281A9D866B2A','415CB898-18DD-4FAE-93FB-FA7A43A61A12','2024-02-02',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('F38CE8D4-0A6B-4FCA-8DA5-FBE5B0390EFA','57458BFF-DECE-4A0F-90F4-0921454B78CA','89F6763A-0852-4E87-BA6E-B6A51EF00C6A','2004-04-04',NULL,1,0)
INSERT INTO [dbo].[Many_Role_RoleTemplates]([RolePermissionId],[Id],[RoleTemplateId],[CreateDate],[Status],[IsDeleted] VALUES('E421C8E9-88A5-4A6E-B096-FCAE315C83C6','43404319-1A6E-4B1C-9EF4-7CDE67BD5AC6','64536B6D-E77E-4EFE-AD19-10A0793476E0','2023-11-30',NULL,1,0)


*/

/*
 * 
 * 
 * 
 *
 
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('57458BFF-DECE-4A0F-90F4-0921454B78CA','Member','2005-05-05 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('1C99C0F7-46DB-4F9A-9A9D-0A8AAA64E367','AgentManagement','2024-02-04 18:11:22.7276968',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('7617105C-C670-421D-9504-0DB4291DC23E','CommentManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('6CEF35F1-3CD0-4088-A29E-0F6BAD075AC6','LayoutManagement','2024-02-04 18:11:22.7277736',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('C2CDF474-E43F-47A3-81A8-145599B554A5','CustomerManagement','2024-02-04 18:11:22.7277723',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('B08EED73-45C5-49F2-B0B6-158B16079BD0','SeoManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('19A86E28-3467-4B4E-ADD2-225F638A2CE3','PersonPolicyManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('30D1572A-1934-4427-A2B9-25983AC605A6','ContactInfoManagement','2024-02-04 18:11:22.7277720',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('F952DC37-C2CC-4842-B32F-27E2A0682EF6','CustomTourManagement','2024-02-04 18:11:22.7277724',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('8043A118-D288-4ACC-8098-281A9D866B2A','TagManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('3FA0B14B-A7D9-40B7-AFBF-2843FF1C9415','FaqCategoryManagement','2024-02-04 18:11:22.7277729',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('9D4CD8CA-4235-4283-9FC5-2DDF05408BAE','SubscriberManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('2E5B434F-F058-4833-93E4-3221A55A2FF4','ReservationManagement','2024-02-04 18:11:22.7277741',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('0B138A9B-C0C3-4124-A566-38AB5360556C','CertificateManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('6A6453A9-1B98-473A-85BD-43A0129E731E','InfoCardManagement','2024-02-04 18:11:22.7277734',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('CCEDABEB-76FE-4B81-B3CB-4591DAA153A2','AuthManagement','2024-02-04 18:11:22.7277702',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('79894F67-568E-4061-8F2B-4727AFBF0B86','Customer','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('71C0207D-7A39-4A1A-A886-53362C9ED6D2','RoleManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('D08D1655-9C58-4015-8EAB-54729CC157FD','SurveyManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('71404E68-5167-4201-B231-593A836B73A3','HomeManagement','2024-02-04 18:11:22.7277733',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('C8F309CE-2DD9-49D3-B08B-5C599636A30F','CancellationPolicyManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('9D61790A-DD39-4023-BBB9-637FDE053533','AdditionalServiceManagement','2005-05-05 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('D1BAD32F-1243-42EF-A897-6833744691B2','ProductManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('CEE76EA5-C1C9-4627-89E7-699B69088246','Guide','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('4CDD773A-AF1B-49F8-87DA-6B4B0DBA949C','VehicleManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('97CC9BC0-63E1-487F-94BB-6DF9EC39C9C8','Agent','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('5CC8035A-71B9-4E8A-B3E2-79D902A66836','ContactInformationManagement','2009-09-08 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('43404319-1A6E-4B1C-9EF4-7CDE67BD5AC6','AgentAccounter','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('50410BD2-849D-456C-A700-7DB1E9FECC37','TourCategoryManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('BDDA3CF1-2AFA-41A7-B4B6-85D1977A4BF2','BlogManagement','2008-08-08 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('AA40BCE2-CB51-4D84-A5BC-89649BFF307F','TourManagement','2024-02-04 18:11:22.7277748',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('9EA9B290-A91B-4E24-8E23-92715FCEF99D','MailManagement','2024-02-04 18:11:22.7277737',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('AA26CA5F-47FC-41F0-8DAC-999041EA811F','DestinationManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('4AC6F423-7712-41F2-A868-9AEEEF805BCE','Driver','2023-12-28 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('68F82810-639B-4927-8052-A321DC21E5D3','SystemOptionManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('EA274801-2F76-41C1-BD86-ABB298255F83','AgentDriver','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('FB5D5277-9645-4169-B18E-AD94D47FD9A2','ChildPolicyManagement','2009-08-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('01B01966-FBC7-440E-B500-B26E7C220F46','Admin','2007-07-07 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('986FF84C-9EB9-4065-A8A3-B4286CB9A98E','DiscountManagement','2024-02-04 18:11:22.7277726',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('95486040-C8BE-4F6C-8CD4-BB18AED50958','CustomBaseManagement','2024-02-04 18:11:22.7277722',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('1E6CFA88-6560-47B1-BFA7-BC2FE9F38725','FaqManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('D306444E-261E-4A38-B32F-C137FCBDE9ED','ImageManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('B4779E75-87C6-45A6-B477-CB27CB644F55','DriverManagement','2024-02-04 18:11:22.7277727',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('18659070-C64D-41AD-88F6-D23268C269C5','ServiceManagement','2024-02-04 18:11:22.7277742',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('7A68A1F2-4BD2-4A72-88C5-D7C6813D65C7','TourGuideManagement','2024-02-04 18:11:22.7277749',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('37FBCE88-DBAA-4D5E-A6D2-E1B7B22EF1C7','CustomPageManagement','2009-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('763B9849-221D-4CD4-BD25-E2336D4EA744','AgentGuide','2023-11-30 12:20:59.1433333',20231,1,3)0 12:20:59.1433333;1;0
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('C158E2C8-9C2D-4EE0-9C1D-E34D366A759C','ConstantValueManagement','2002-08-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('D5AE0B57-8F02-4268-9F76-E633E5676F21','ContactMessageManagement','2008-08-08 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('C64B2AF7-E852-4EF8-BB58-E9B85D3809AC','BlogCategoryManagement','2024-02-04 18:11:22.7277719',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('F1314CEE-8B18-4741-A8C7-EA439905151D','SupplierManagement','2024-02-04 18:11:22.7277745',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('094AB07B-26DB-456A-BEF2-FA11A24EACCE','UserManagement','2000-09-09 00:00:00.0000000',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('53871E30-CD5A-4E17-91ED-FCF8027C3299','OperationManagement','2024-02-04 18:11:22.7277739',NULL,1,0)
INSERT INTO [dbo].[RolePermissions]([Id],[Name],[CreateDate],[UpdateDate],[Status],[IsDeleted]) VALUES ('62B364F4-B722-400E-A747-FE33FD4025B6','InformationCardManagement','2000-09-09 00:00:00.0000000',NULL,1,0)



 * */