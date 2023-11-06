<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmbedConfigErrorLog.aspx.cs" Inherits="BoldBIEmbedSample.EmbedConfigErrorLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="~/Content/Site.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js" type="text/javascript"></script>
    <link href="https://cdn.syncfusion.com/ej2/material.css" rel="stylesheet" />
</head>
<body onload="embedConfigErrorDialog()">
    <script type="text/javascript">
        function embedConfigErrorDialog() {
            var targetContainer = $('<div id="custom_dialog"></div>');
            var dlgDiv = $('<div id="sample_dialog" ></div>');
            targetContainer.append(dlgDiv);
            $('body').append(targetContainer);
            var dialog = new window.ejs.popups.Dialog({
                header: 'Error Message',
                width: '500px',
                height: '156px',
                isModal: true,
                showCloseIcon: true,
                target: document.getElementById('custom_dialog'),
                content: '<div>To compile and run the project, an embed config file needs to be required. Please use the <a href="https://help.boldbi.com/site-administration/embed-settings/" target="_blank">URL</a> to obtain the JSON file from the Bold BI server.</div>'
            });
            dialog.appendTo('#sample_dialog');
            var dialogFooter = $('<div id="sample_dialog_footer"><button id="custom_ok_button"onclick="Cancel()">OK</button></div>')
            $('#sample_dialog').append(dialogFooter);
            $('.e-dlg-overlay').css('position', 'fixed');
        };

        function Cancel() {
            $("#custom_dialog").html('');
        }
    </script>
</body>
</html>


