using System;
using System.Threading.Tasks;
using AutomationFrameWork.Utilities;
using Microsoft.Playwright;

namespace AutomationFrameWork.CodegenAI.AIRecorder
{
    public sealed class CodegenRecordedFlow
    {
        public CodegenRecordedFlow(IPage page, ConfigReader config)
        {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IPage Page { get; }

        public ConfigReader Config { get; }

        public async Task ReplayAsync()
        {
            // Source: Code.ts line 4
            // Review: URL at source line uses literal value because no matching appsettings Urls entry was found: https://login.microsoftonline.com/0b14651e-5110-47c7-b458-14565f1d46de/oauth2/v2.0/authorize?client_id=a0902dc2-534e-4656-af5f-fae202af26dd&redirect_uri=https%3A%2F%2Finvestorreporting-mb-sit.trimont.com%2Fsignin-oidc&response_type=code&scope=openid%20profile%20email&code_challenge=91FBkVIlc40A360qo_tBDc1LKsgEtjphcCwDujIxo68&code_challenge_method=S256&state=OpenIdConnect.AuthenticationProperties%3DUdp2K7tgTyhY8hNjdHwkuXedDjJW3JaXoA8mlgqeiHrwbtBMuLeSGs_77dOZE_Z_2enNbiosZ747cFPy0IbQMah0rolKT-Gw4MtLb2zoqA2sZ646BtHov0zi-JfZznUP3kbfV47cxqXowMajtUfpudD8cN3KjfrfYIvU4nPhEESAoTdWFnnVP3hBBJgWfxRvGXOYL4bOv_IRnQ17MqjCGSQoaA_zfVYTI45ytOkdSA_wp9eq0WhZNUVX8lisuqiyeVf0wxdJj58Fgv-zeK5fhCzst1LWbk7060Y2yKjV_8wvMc56z5U9eB0sIW7oTcXKGe8f-kjJSSVKOIYlsp4b7WBjx5fq-w_VNECMMhw2K8Y&response_mode=form_post&nonce=639258396838190451.NDAxMTEwM2EtNGRhYS00MjdmLTk3YmQtMmMxYTJlODliZjI2ZjdlNmI5NmEtZTY4Mi00ZTk1LTg1MWMtZWVlOWM5YzdjNGQx&x-client-SKU=ID_NET472&x-client-ver=7.5.0.0&sso_reload=true
            await Page.GotoAsync("https://login.microsoftonline.com/0b14651e-5110-47c7-b458-14565f1d46de/oauth2/v2.0/authorize?client_id=a0902dc2-534e-4656-af5f-fae202af26dd&redirect_uri=https%3A%2F%2Finvestorreporting-mb-sit.trimont.com%2Fsignin-oidc&response_type=code&scope=openid%20profile%20email&code_challenge=91FBkVIlc40A360qo_tBDc1LKsgEtjphcCwDujIxo68&code_challenge_method=S256&state=OpenIdConnect.AuthenticationProperties%3DUdp2K7tgTyhY8hNjdHwkuXedDjJW3JaXoA8mlgqeiHrwbtBMuLeSGs_77dOZE_Z_2enNbiosZ747cFPy0IbQMah0rolKT-Gw4MtLb2zoqA2sZ646BtHov0zi-JfZznUP3kbfV47cxqXowMajtUfpudD8cN3KjfrfYIvU4nPhEESAoTdWFnnVP3hBBJgWfxRvGXOYL4bOv_IRnQ17MqjCGSQoaA_zfVYTI45ytOkdSA_wp9eq0WhZNUVX8lisuqiyeVf0wxdJj58Fgv-zeK5fhCzst1LWbk7060Y2yKjV_8wvMc56z5U9eB0sIW7oTcXKGe8f-kjJSSVKOIYlsp4b7WBjx5fq-w_VNECMMhw2K8Y&response_mode=form_post&nonce=639258396838190451.NDAxMTEwM2EtNGRhYS00MjdmLTk3YmQtMmMxYTJlODliZjI2ZjdlNmI5NmEtZTY4Mi00ZTk1LTg1MWMtZWVlOWM5YzdjNGQx&x-client-SKU=ID_NET472&x-client-ver=7.5.0.0&sso_reload=true");

            // Source: Code.ts line 5
            // Review: URL at source line uses literal value because no matching appsettings Urls entry was found: https://login.microsoftonline.com/0b14651e-5110-47c7-b458-14565f1d46de/login
            await Page.GotoAsync("https://login.microsoftonline.com/0b14651e-5110-47c7-b458-14565f1d46de/login");

            // Source: Code.ts line 6
            // Review: URL at source line uses literal value because no matching appsettings Urls entry was found: https://login.microsoftonline.com/common/SAS/ProcessAuth
            await Page.GotoAsync("https://login.microsoftonline.com/common/SAS/ProcessAuth");

            // Source: Code.ts line 7
            // Review: URL at source line uses literal value because no matching appsettings Urls entry was found: https://investorreporting-mb-sit.trimont.com/default.aspx
            await Page.GotoAsync("https://investorreporting-mb-sit.trimont.com/default.aspx");

        }
    }
}