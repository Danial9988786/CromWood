using System.Net;
using System.Net.Mail;

namespace CromWood.Helper
{
    public static class EmailSender
    {
        public static void SendInvitationEmail(string email, string password)
        {
            string mailbody = $@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html dir=""ltr"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""en"">
 <head>
  <meta charset=""UTF-8"">
  <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
  <meta name=""x-apple-disable-message-reformatting"">
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta content=""telephone=no"" name=""format-detection"">
  <title>Empty template</title><!--[if (mso 16)]>
    <style type=""text/css"">
    a {{text-decoration: none;}}
    </style>
    <![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>
<xml>
    <o:OfficeDocumentSettings>
    <o:AllowPNG></o:AllowPNG>
    <o:PixelsPerInch>96</o:PixelsPerInch>
    </o:OfficeDocumentSettings>
</xml>
<![endif]-->
  <style type=""text/css"">
.jfk-bubble.gtx-bubble, .captcheck_answer_label > input + img, span#closed_text > img[src^=""https://www.gstatic.com/images/branding/googlelogo""], span[data-href^=""https://www.hcaptcha.com/""] > #icon, #bit-notification-bar-iframe, ::-webkit-calendar-picker-indicator, div[role=""dialog""] div[role=""document""], div[role=""dialog""] div[role=""document""], div[role=""dialog""] div[role=""document""], div[role=""dialog""] div[role=""document""], div[role=""dialog""] div[role=""document""], div[role=""dialog""] div[role=""document""] {{
    filter: invert(100%) hue-rotate(180deg) brightness(95%) contrast(90%) !important;
}}
[data-darkreader-inline-bgcolor] {{
  background-color: var(--darkreader-inline-bgcolor) !important;
}}
[data-darkreader-inline-bgimage] {{
  background-image: var(--darkreader-inline-bgimage) !important;
}}
[data-darkreader-inline-border] {{
  border-color: var(--darkreader-inline-border) !important;
}}
[data-darkreader-inline-border-bottom] {{
  border-bottom-color: var(--darkreader-inline-border-bottom) !important;
}}
[data-darkreader-inline-border-left] {{
  border-left-color: var(--darkreader-inline-border-left) !important;
}}
[data-darkreader-inline-border-right] {{
  border-right-color: var(--darkreader-inline-border-right) !important;
}}
[data-darkreader-inline-border-top] {{
  border-top-color: var(--darkreader-inline-border-top) !important;
}}
[data-darkreader-inline-boxshadow] {{
  box-shadow: var(--darkreader-inline-boxshadow) !important;
}}
[data-darkreader-inline-color] {{
  color: var(--darkreader-inline-color) !important;
}}
[data-darkreader-inline-fill] {{
  fill: var(--darkreader-inline-fill) !important;
}}
[data-darkreader-inline-stroke] {{
  stroke: var(--darkreader-inline-stroke) !important;
}}
[data-darkreader-inline-outline] {{
  outline-color: var(--darkreader-inline-outline) !important;
}}
[data-darkreader-inline-stopcolor] {{
  stop-color: var(--darkreader-inline-stopcolor) !important;
}}
:root {{
   --darkreader-neutral-background: #121415;
   --darkreader-neutral-text: #cdc9c5;
   --darkreader-selection-background: #0049a2;
   --darkreader-selection-text: #dcdbd8;
}}
html {{
    color-scheme: dark !important;
}}
html, body {{
    border-color: #6d6659;
    color: #dcdbd8;
}}
a {{
    color: #308af2;
}}
table {{
    border-color: #505659;
}}
::placeholder {{
    color: #a9a299;
}}
input:-webkit-autofill,
textarea:-webkit-autofill,
select:-webkit-autofill {{
    background-color: #3d4100 !important;
    color: #dcdbd8 !important;
}}
::-webkit-scrollbar {{
    background-color: #1e2122;
    color: #a29c91;
}}
::-webkit-scrollbar-thumb {{
    background-color: #424649;
}}
::-webkit-scrollbar-thumb:hover {{
    background-color: #53595d;
}}
::-webkit-scrollbar-thumb:active {{
    background-color: #444a4d;
}}
::-webkit-scrollbar-corner {{
    background-color: #17191a;
}}
::selection {{
    background-color: #0049a2 !important;
    color: #dcdbd8 !important;
}}
::-moz-selection {{
    background-color: #0049a2 !important;
    color: #dcdbd8 !important;
}}
#outlook a {{
	padding:0;
}}
.es-button {{
	mso-style-priority:100!important;
	text-decoration:none!important;
}}
a[x-apple-data-detectors] {{
	color:inherit!important;
	text-decoration:none!important;
	font-size:inherit!important;
	font-family:inherit!important;
	font-weight:inherit!important;
	line-height:inherit!important;
}}
.es-desk-hidden {{
	display:none;
	float:left;
	overflow:hidden;
	width:0;
	max-height:0;
	line-height:0;
	mso-hide:all;
}}
@media only screen and (max-width:600px) {{p, ul li, ol li, a {{ line-height:150%!important }} h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120%!important }} h1 {{ font-size:30px!important; text-align:left }} h2 {{ font-size:24px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:30px!important; text-align:left }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:24px!important; text-align:left }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important; text-align:left }} .es-menu td a {{ font-size:14px!important }} .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }} *[class=""gmail-fix""] {{ display:none!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-button-border {{ display:inline-block!important }} a.es-button, button.es-button {{ font-size:18px!important; display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .es-adapt-td {{ display:block!important; width:100%!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-m-p0 {{ padding:0px!important }} .es-m-p0r {{ padding-right:0px!important }} .es-m-p0l {{ padding-left:0px!important }} .es-m-p0t {{ padding-top:0px!important }} .es-m-p0b {{ padding-bottom:0!important }} .es-m-p20b {{ padding-bottom:20px!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} table.es-social {{ display:inline-block!important }} table.es-social td {{ display:inline-block!important }} .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }} }}
@media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}
</style>
 </head>
 <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
  <div dir=""ltr"" class=""es-wrapper-color"" lang=""en"" style=""background-color:#F6F6F6""><!--[if gte mso 9]>
			<v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				<v:fill type=""tile"" color=""#f6f6f6""></v:fill>
			</v:background>
		<![endif]-->
   <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#F6F6F6"">
     <tr>
      <td valign=""top"" style=""padding:0;Margin:0"">
       <table class=""es-header"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table class=""es-header-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" data-darkreader-inline-bgcolor style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:45px;color:#333333;font-size:30px""><strong><span>You are invited</span></strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table class=""es-content-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" data-darkreader-inline-bgcolor style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
               <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                   <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px;text-align:justify""><strong>Hello there,</strong><br><br>Cromwood admin have invited you as a new member to their team to collabrate with them.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px;text-align:justify""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px;text-align:justify"">You have been assigned a default password along with username as your email.<br><br><span style=""color:#000080"" data-darkreader-inline-color>Username: {email}<br>Password: {password}</span></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><span style=""font-size:12px""><em>Note: We recommend you to change the default password as you login for the first time.</em></span><br><br>Thanks,<br>Team Cromwood</p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                       <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #cccccc;background:unset;height:1px;width:100%;margin:0px"" data-darkreader-inline-border-bottom data-darkreader-inline-bgimage data-darkreader-inline-bgcolor></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table></td>
     </tr>
   </table>
  </div>
 </body>
</html>
";

            var fromAddress = new MailAddress("ananta.caresort@gmail.com", "Ananta Poudel");
            var toAddress = new MailAddress(email);
            const string fromPassword = "vqlz osnj bcce imyc";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Invitation to join CromWood",
                Body = mailbody,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

        public static void SendOTPEmail(string email, string otp)
        {
            string mailbody = $@"
                    Your OTP: {otp}
                    ";

            var fromAddress = new MailAddress("ananta.caresort@gmail.com", "Ananta Poudel");
            var toAddress = new MailAddress(email);
            const string fromPassword = "vqlz osnj bcce imyc";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Verify OTP",
                Body = mailbody,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
