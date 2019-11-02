#pragma checksum "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf88817b89c9576ac2bcbafbe11585aa738be190"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_CreateEmployeeList), @"mvc.1.0.view", @"/Views/Home/CreateEmployeeList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/CreateEmployeeList.cshtml", typeof(AspNetCore.Views_Home_CreateEmployeeList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\_ViewImports.cshtml"
using DemoAccountingSystem;

#line default
#line hidden
#line 2 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\_ViewImports.cshtml"
using DemoAccountingSystem.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf88817b89c9576ac2bcbafbe11585aa738be190", @"/Views/Home/CreateEmployeeList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c9c686a2355857532fcb82f5171900ae88449d6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_CreateEmployeeList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CreateEmployListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
  
    ViewData["Title"] = "CreateEmployeeList";

#line default
#line hidden
            BeginContext(88, 258, true);
            WriteLiteral(@"
<h1>New Employees</h1>

<table class=""table table-striped"">
    <thead>
        <tr>
            <th scope=""col"">First name</th>
            <th scope=""col"">Last name</th>
            <th scope=""col""></th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 17 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
         foreach (var item in Model.RemoteEmployees)
        {

#line default
#line hidden
            BeginContext(411, 60, true);
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(472, 14, false);
#line 21 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
               Write(item.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(486, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(554, 13, false);
#line 24 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
               Write(item.LastName);

#line default
#line hidden
            EndContext();
            BeginContext(567, 67, true);
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(635, 65, false);
#line 27 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
               Write(Html.ActionLink("Create", "CreateEmployee", new { id = item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(700, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 30 "D:\c#\AccountingSystem\DemoAccountingSystem\Views\Home\CreateEmployeeList.cshtml"
        }

#line default
#line hidden
            BeginContext(755, 26, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CreateEmployListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
