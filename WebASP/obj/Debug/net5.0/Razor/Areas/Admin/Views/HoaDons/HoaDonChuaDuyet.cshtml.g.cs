#pragma checksum "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d50eb57995856521ba4fa0731b3ac7becb81f589"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_HoaDons_HoaDonChuaDuyet), @"mvc.1.0.view", @"/Areas/Admin/Views/HoaDons/HoaDonChuaDuyet.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d50eb57995856521ba4fa0731b3ac7becb81f589", @"/Areas/Admin/Views/HoaDons/HoaDonChuaDuyet.cshtml")]
    public class Areas_Admin_Views_HoaDons_HoaDonChuaDuyet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebASP.Models.HoaDon>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
  
    ViewData["TaiKhoan"] = ViewBag.TaiKhoan;
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Tất cả hóa đơn chưa duyệt</h1>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.MAHD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.TaiKhoan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.NgayLap));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.ThanhToan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 26 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.DChiGiaoHang));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 29 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.SDTGiaoHang));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.TenNguoiNhan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 35 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.TongTien));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
           Write(Html.DisplayNameFor(model => model.TrangThai));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 44 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 48 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.MAHD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.TaiKhoan.HoTen));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 54 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.NgayLap));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 57 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.ThanhToan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 60 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.DChiGiaoHang));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 63 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.SDTGiaoHang));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 66 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.TenNguoiNhan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 69 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
               Write(Html.DisplayFor(modelItem => item.TongTien));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n\r\n                    <a asp-controller=\"HoaDons\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 2283, "\"", 2312, 1);
#nullable restore
#line 73 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
WriteAttributeValue("", 2298, item.HoaDonId, 2298, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" asp-route-trang=\"HoaDonChuaDuyet\" asp-action=\"SuaTrangThai\">\r\n");
#nullable restore
#line 74 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
                         if (item.TrangThai == true)
                        {
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"submit\" class=\"btn btn-success\">True</button>\r\n");
#nullable restore
#line 78 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <button type=\"submit\" class=\"btn btn-danger\">False</button>\r\n");
#nullable restore
#line 82 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </a>\r\n                </td>\r\n                <td>\r\n                    <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 2887, "\"", 2916, 1);
#nullable restore
#line 86 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
WriteAttributeValue("", 2902, item.HoaDonId, 2902, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Sửa</a> |\r\n                    <a asp-action=\"Details\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 2972, "\"", 3001, 1);
#nullable restore
#line 87 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
WriteAttributeValue("", 2987, item.HoaDonId, 2987, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Chi tiết</a> |\r\n                    <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 3061, "\"", 3090, 1);
#nullable restore
#line 88 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
WriteAttributeValue("", 3076, item.HoaDonId, 3076, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Xóa</a>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 91 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\HoaDonChuaDuyet.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebASP.Models.HoaDon>> Html { get; private set; }
    }
}
#pragma warning restore 1591
