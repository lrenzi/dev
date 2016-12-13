using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterLayout(bundles);
            RegisterApp(bundles);
            RegisterAngular(bundles);
            RegisterJquery(bundles);
            RegisterBootstrap(bundles);

            //telas
            RegisterAccount(bundles);
            RegisterFileUpload(bundles);
        }

       
        private static void RegisterJquery(BundleCollection bundles)
        {         // plugins | jquery
            bundles.Add(new ScriptBundle("~/plugins/jquery/js").Include(
                                             "~/plugins/jquery/js/jquery-3.1.0.js"));

            // plugins | jquery-validate
            bundles.Add(new ScriptBundle("~/plugins/jquery-validate/js").Include(
                                         "~/plugins/jquery-validate/js/jquery.validate*"));

            // plugins | jquery-ui
            bundles.Add(new ScriptBundle("~/plugins/jquery-ui/js").Include(
                                         "~/plugins/jquery-ui/js/jquery-ui.min.js"));

        }

        private static void RegisterBootstrap(BundleCollection bundles)
        {
            // bootbox
            bundles.Add(new ScriptBundle("~/plugins/bootstrap/js").Include(
                "~/plugins/bootstrap/js/bootbox.min.js"));

            // bootstrap
            bundles.Add(new ScriptBundle("~/plugins/bootstrap/js").Include(
                "~/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/plugins/bootstrap/css").Include(
                "~/plugins/bootstrap/css/bootstrap.min.css"));
        }
       

        private static void RegisterAngular(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/plugins/angular/angular-route").Include(
                "~/plugins/angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/plugins/angular").Include(
                "~/plugins/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/plugins/angular/progress").Include(
                "~/plugins/angular/progress.js"));
        }

        private static void RegisterApp(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/app").Include(
                "~/Scripts/app.js"));
        }

        private static void RegisterLayout(BundleCollection bundles)
        {
            // dist
            bundles.Add(new ScriptBundle("~/plugins/dist/js").Include(
                "~/plugins/dist/js/app.js"));

            bundles.Add(new StyleBundle("~/plugins/dist/css").Include(
                "~/plugins/dist/css/admin-lte.min.css"));

            bundles.Add(new StyleBundle("~/plugins/dist/css/skins").Include(
                "~/plugins/dist/css/skins/_all-skins.min.css"));

            // documentation
            bundles.Add(new ScriptBundle("~/plugins/documentation/js").Include(
                "~/plugins/documentation/js/docs.js"));

            bundles.Add(new StyleBundle("~/plugins/documentation/css").Include(
                "~/plugins/documentation/css/style.css"));

            // plugins | slimscroll
            bundles.Add(new ScriptBundle("~/plugins/slimscroll/js").Include(
                                         "~/plugins/slimscroll/js/jquery.slimscroll.min.js"));

            // plugins | font-awesome
            bundles.Add(new StyleBundle("~/plugins/font-awesome/css").Include(
                                        "~/plugins/font-awesome/css/font-awesome.min.css"));


            // plugins | bootstrap-slider
            bundles.Add(new ScriptBundle("~/plugins/bootstrap-slider/js").Include(
                                        "~/plugins/bootstrap-slider/js/bootstrap-slider.js"));

            bundles.Add(new StyleBundle("~/plugins/bootstrap-slider/css").Include(
                                        "~/plugins/bootstrap-slider/css/slider.css"));

            // plugins | bootstrap-wysihtml5
            bundles.Add(new ScriptBundle("~/plugins/bootstrap-wysihtml5/js").Include(
                                         "~/plugins/bootstrap-wysihtml5/js/bootstrap3-wysihtml5.all.min.js"));

            bundles.Add(new StyleBundle("~/plugins/bootstrap-wysihtml5/css").Include(
                                        "~/plugins/bootstrap-wysihtml5/css/bootstrap3-wysihtml5.min.css"));

            // plugins | chartjs
            bundles.Add(new ScriptBundle("~/plugins/chartjs/js").Include(
                                         "~/plugins/chartjs/js/chart.min.js"));

            // plugins | ckeditor
            bundles.Add(new ScriptBundle("~/plugins/ckeditor/js").Include(
                                         "~/plugins/ckeditor/js/ckeditor.js"));

            // plugins | colorpicker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker/js").Include(
                                         "~/plugins/colorpicker/js/bootstrap-colorpicker.min.js"));

            bundles.Add(new StyleBundle("~/plugins/colorpicker/css").Include(
                                        "~/plugins/colorpicker/css/bootstrap-colorpicker.css"));

            // plugins | datatables
            bundles.Add(new ScriptBundle("~/plugins/datatables/js").Include(
                                         "~/plugins/datatables/js/jquery.dataTables.min.js",
                                         "~/plugins/datatables/js/dataTables.bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/plugins/datatables/css").Include(
                                        "~/plugins/datatables/css/dataTables.bootstrap.css"));

            // plugins | datepicker
            bundles.Add(new ScriptBundle("~/plugins/datepicker/js").Include(
                                         "~/plugins/datepicker/js/bootstrap-datepicker.js",
                                         "~/plugins/datepicker/js/locales/bootstrap-datepicker*"));

            bundles.Add(new StyleBundle("~/plugins/datepicker/css").Include(
                                        "~/plugins/datepicker/css/datepicker3.css"));

            // plugins | daterangepicker
            bundles.Add(new ScriptBundle("~/plugins/daterangepicker/js").Include(
                                         "~/plugins/daterangepicker/js/moment.min.js",
                                         "~/plugins/daterangepicker/js/daterangepicker.js"));

            bundles.Add(new StyleBundle("~/plugins/daterangepicker/css").Include(
                                        "~/plugins/daterangepicker/css/daterangepicker-bs3.css"));

            // plugins | fastclick
            bundles.Add(new ScriptBundle("~/plugins/fastclick/js").Include(
                                         "~/plugins/fastclick/js/fastclick.min.js"));

            // plugins | flot
            bundles.Add(new ScriptBundle("~/plugins/flot/js").Include(
                                         "~/plugins/flot/js/jquery.flot.min.js",
                                         "~/plugins/flot/js/jquery.flot.resize.min.js",
                                         "~/plugins/flot/js/jquery.flot.pie.min.js",
                                         "~/plugins/flot/js/jquery.flot.categories.min.js"));

           
            // plugins | fullcalendar
            bundles.Add(new ScriptBundle("~/plugins/fullcalendar/js").Include(
                                         "~/plugins/fullcalendar/js/fullcalendar.min.js"));

            bundles.Add(new StyleBundle("~/plugins/fullcalendar/css").Include(
                                        "~/plugins/fullcalendar/css/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/plugins/fullcalendar/css/print").Include(
                                        "~/plugins/fullcalendar/css/print/fullcalendar.print.css"));

            // plugins | icheck
            bundles.Add(new ScriptBundle("~/plugins/icheck/js").Include(
                                         "~/plugins/icheck/js/icheck.min.js"));

            bundles.Add(new StyleBundle("~/plugins/icheck/css").Include(
                                        "~/plugins/icheck/css/all.css"));

            bundles.Add(new StyleBundle("~/plugins/icheck/css/flat").Include(
                                        "~/plugins/icheck/css/flat/flat.css"));

            bundles.Add(new StyleBundle("~/plugins/icheck/css/sqare/blue").Include(
                                        "~/plugins/icheck/css/sqare/blue.css"));

            // plugins | input-mask
            bundles.Add(new ScriptBundle("~/plugins/input-mask/js").Include(
                                         "~/plugins/input-mask/js/jquery.inputmask.js",
                                         "~/plugins/input-mask/js/jquery.inputmask.date.extensions.js",
                                         "~/plugins/input-mask/js/jquery.inputmask.extensions.js"));

            // plugins | ionicons
            bundles.Add(new StyleBundle("~/plugins/ionicons/css").Include(
                                        "~/plugins/ionicons/css/ionicons.min.css"));

            // plugins | ionslider
            bundles.Add(new ScriptBundle("~/plugins/ionslider/js").Include(
                                         "~/plugins/ionslider/js/ion.rangeSlider.min.js"));

            bundles.Add(new StyleBundle("~/plugins/ionslider/css").Include(
                                        "~/plugins/ionslider/css/ion.rangeSlider.css",
                                        "~/plugins/ionslider/css/ion.rangeSlider.skinNice.css"));

            

            

            // plugins | jvectormap
            bundles.Add(new ScriptBundle("~/plugins/jvectormap/js").Include(
                                         "~/plugins/jvectormap/js/jquery-jvectormap-1.2.2.min.js",
                                         "~/plugins/jvectormap/js/jquery-jvectormap-world-mill-en.js"));

            bundles.Add(new StyleBundle("~/plugins/jvectormap/css").Include(
                                        "~/plugins/jvectormap/css/jquery-jvectormap-1.2.2.css"));

            // plugins | knob
            bundles.Add(new ScriptBundle("~/plugins/knob/js").Include(
                                         "~/plugins/knob/js/jquery.knob.js"));

            // plugins | morris
            bundles.Add(new StyleBundle("~/plugins/morris/css").Include(
                                        "~/plugins/morris/css/morris.css"));

            // plugins | momentjs
            bundles.Add(new ScriptBundle("~/plugins/momentjs/js").Include(
                                         "~/plugins/momentjs/js/moment.min.js"));

            // plugins | pace
            bundles.Add(new ScriptBundle("~/plugins/pace/js").Include(
                                         "~/plugins/pace/js/pace.min.js"));

            bundles.Add(new StyleBundle("~/plugins/pace/css").Include(
                                        "~/plugins/pace/css/pace.min.css"));

            

            // plugins | sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline/js").Include(
                                         "~/plugins/sparkline/js/jquery.sparkline.min.js"));

            // plugins | timepicker
            bundles.Add(new ScriptBundle("~/plugins/timepicker/js").Include(
                                         "~/plugins/timepicker/js/bootstrap-timepicker.min.js"));

            bundles.Add(new StyleBundle("~/plugins/timepicker/css").Include(
                                        "~/plugins/timepicker/css/bootstrap-timepicker.min.css"));

            // plugins | raphael
            bundles.Add(new ScriptBundle("~/plugins/raphael/js").Include(
                                         "~/plugins/raphael/js/raphael-min.js"));

            // plugins | select2
            bundles.Add(new ScriptBundle("~/plugins/select2/js").Include(
                                         "~/plugins/select2/js/select2.full.min.js"));

            bundles.Add(new StyleBundle("~/plugins/select2/css").Include(
                                        "~/plugins/select2/css/select2.min.css"));

            // plugins | morris
            bundles.Add(new ScriptBundle("~/plugins/morris/js").Include(
                                         "~/plugins/morris/js/morris.min.js"));
                                         
        }

        //Telas

        private static void RegisterAccount(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Account/Login").Include(
                "~/Scripts/Core/Account/Login.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Account/Register").Include(
                "~/Scripts/Core/Account/Register.js"));
        }

        private static void RegisterFileUpload(BundleCollection bundles)
        {         // plugins | jquery
            bundles.Add(new ScriptBundle("~/Scripts/Controllers/FileUpload").Include(
                                             "~/Scripts/Controllers/FileUploadController.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Service/FileUpload").Include(
                                             "~/Scripts/Service/FileUploadService.js"));

        }

    }
}
