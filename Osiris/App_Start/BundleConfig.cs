﻿using System.Web;
using System.Web.Optimization;

namespace Osiris
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkId=301862 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*",
                        "~/Scripts/datepicker-ja.js",
                        "~/Scripts/sweetalert.min.js"));

            // 開発と学習には、Modernizr の開発バージョンを使用します。次に、実稼働の準備が
            // 運用の準備が完了したら、https://modernizr.com のビルド ツールを使用し、必要なテストのみを選択します。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/umd/popper.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery-ui.min.css",
                      "~/Content/bootstrap.css",
                      //"~/Content/theme.default.min.css",
                      "~/Content/all.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/cmnlib").Include(
                        "~/Scripts/common.js",
                        "~/Scripts/pace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tablesorter").Include(
                        "~/Scripts/jquery.tablesorter.min.js"));
        }
    }
}
