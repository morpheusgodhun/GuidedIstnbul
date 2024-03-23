using Core.Entities;
using Core.IRepository;
using Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Many_Role_RoleTemplateRepository : GenericRepository<Many_Role_RoleTemplate>, IMany_Role_RoleTemplateRepository
    {
        public Many_Role_RoleTemplateRepository(Context context) : base(context)
        {
        }

        public List<RolePermissionDto> GetRoleTemplatePermissions()
        {
            var query = from mrt in _context.Many_Role_RoleTemplates
                        join rt in _context.RoleTemplates on mrt.RoleTemplateId equals rt.Id
                        join r in _context.RolePermissions on mrt.RolePermissionId equals r.Id
                        orderby rt.Name
                        group r.Name by rt.Name into g
                        select new RolePermissionDto
                        {
                            RoleTemplateName = g.Key,
                            Permissions = g.ToList()
                        };

            var data = query.ToList();
            return data;
        }
    }
}
/*
 * 
 * 
INSERT INTO Many_Role_RoleTemplates VALUES(22bc37d7-c2ba-4f15-9c8c-697df91c245e	7617105c-c670-421d-9504-0db4291dc23e	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(19242b9e-53d9-4cbd-8fa7-b2fe0e62519b	b08eed73-45c5-49f2-b0b6-158b16079bd0	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(50808091-e4ee-4041-ab87-8fac9bfd99a1	19a86e28-3467-4b4e-add2-225f638a2ce3	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(1291a8bc-17a2-4584-a007-fb8d1f1813be	8043a118-d288-4acc-8098-281a9d866b2a	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(712d6960-6d0b-4cab-b1c4-b65cea419d80	9d4cd8ca-4235-4283-9fc5-2ddf05408bae	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(1eedf435-9d85-4e60-a858-8195c76e1835	0b138a9b-c0c3-4124-a566-38ab5360556c	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(5c73e242-c721-42b8-ba56-f6f6ed801d25	71c0207d-7a39-4a1a-a886-53362c9ed6d2	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(e121e335-556c-411b-a052-3c949efbbec8	d08d1655-9c58-4015-8eab-54729cc157fd	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(36ac35b2-da58-4600-9290-ab8edf8112a9	c8f309ce-2dd9-49d3-b08b-5c599636a30f	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(b11bbdba-b6c2-4ae8-a43c-ca8b2d8252b4	9d61790a-dd39-4023-bbb9-637fde053533	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(ece680a7-fd7a-4ac3-ba52-bed53bda1502	d1bad32f-1243-42ef-a897-6833744691b2	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(3fa52126-cc1e-4907-8f9c-d8840e51ea33	5cc8035a-71b9-4e8a-b3e2-79d902a66836	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(c516ecd4-bccc-4bc2-a297-a45954347b19	50410bd2-849d-456c-a700-7db1e9fecc37	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(eab188d9-8cef-49f1-b647-721044868b4f	bdda3cf1-2afa-41a7-b4b6-85d1977a4bf2	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(c7f307c0-749e-4f6d-adc6-e67ef5e9506a	aa26ca5f-47fc-41f0-8dac-999041ea811f	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(6381bfd6-7590-47c9-b59b-35b565fc5465	68f82810-639b-4927-8052-a321dc21e5d3	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(9308b05c-18d2-473d-a97b-de15e287a6d7	fb5d5277-9645-4169-b18e-ad94d47fd9a2	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(28ae3d3c-2eea-4b90-be83-dafd7b92f227	1e6cfa88-6560-47b1-bfa7-bc2fe9f38725	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(ede73d94-c62f-4997-b506-7bdca6b22570	d306444e-261e-4a38-b32f-c137fcbde9ed	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(72bd5f7b-bb7a-4be9-b0ae-319b9393f492	37fbce88-dbaa-4d5e-a6d2-e1b7b22ef1c7	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(665fe4be-e12e-4725-8faf-a32ab7031762	c158e2c8-9c2d-4ee0-9c1d-e34d366a759c	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(0de03757-9536-4dfc-8dc5-73df893b5bfa	d5ae0b57-8f02-4268-9f76-e633e5676f21	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(de91c2f7-fdf4-4019-b9cf-27da87908717	094ab07b-26db-456a-bef2-fa11a24eacce	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
INSERT INTO Many_Role_RoleTemplates VALUES(95e15f29-1c99-4e89-8dbd-dbb99424724b	62b364f4-b722-400e-a747-fe33fd4025b6	415cb898-18dd-4fae-93fb-fa7a43a61a12	2024-02-02 00:00:00.0000000	NULL	True	False)
 * */