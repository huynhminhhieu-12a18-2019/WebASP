#pragma checksum "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a4aa9428511915187effcf5f5f9a9906278ae7ee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_LoaiSPs_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/LoaiSPs/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a4aa9428511915187effcf5f5f9a9906278ae7ee", @"/Areas/Admin/Views/LoaiSPs/Index.cshtml")]
    public class Areas_Admin_Views_LoaiSPs_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebASP.Models.LoaiSP>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
  
    ViewData["TaiKhoan"] = ViewBag.TaiKhoan;
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Tất cả loại sản phẩm</h1>\r\n\r\n<p>\r\n    <a asp-action=\"Create\">Thêm mới loại sản phẩm</a>\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 18 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TenLoai));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 24 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 27 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.TenLoai));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 696, "\"", 725, 1);
#nullable restore
#line 30 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
WriteAttributeValue("", 711, item.LoaiSPId, 711, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Sửa</a> |\r\n                <a asp-action=\"Details\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 777, "\"", 806, 1);
#nullable restore
#line 31 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
WriteAttributeValue("", 792, item.LoaiSPId, 792, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Chi tiết</a> |\r\n                <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 862, "\"", 891, 1);
#nullable restore
#line 32 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
WriteAttributeValue("", 877, item.LoaiSPId, 877, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Xóa</a>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 35 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\LoaiSPs\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebASP.Models.LoaiSP>> Html { get; private set; }
    }
}
#pragma warning restore 1591
