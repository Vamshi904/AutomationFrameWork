import { test, expect } from '@playwright/test';

test.use({
  storageState: 'C:\\Users\\vamshi.annam\\Documents\\ScrubDB\\scrubdb-app\\testing\\ScrubDb Playwright Automation\\AutomationFrameWork\\CodegenAI\\AIRecorder\\auth\\session.json'
});

test('test', async ({ page }) => {
  await page.goto('https://login.microsoftonline.com/0b14651e-5110-47c7-b458-14565f1d46de/oauth2/v2.0/authorize?client_id=0ec3f036-66be-4d30-b449-921b8a4e8918&redirect_uri=https%3A%2F%2Fscrubdb-uat.trimont.com%2Fsignin-oidc&response_type=code&scope=openid%20profile%20email&code_challenge=OHHLzgMfIViGou82Hh0uXNPFaTuD9-T0tHOjl78uz6k&code_challenge_method=S256&state=OpenIdConnect.AuthenticationProperties%3DGtKD_Up_DdgjvE9pG-FF0_vDn0xFDkVIqWi6py_CYV789o-JTcvNBaSC-zM5PMskg0ZxmuNVOCCPebofqqubDy1F5V2XSnpZ1QL28b1--U41zUcWdE3Dhcdiek8FNK0yU1srIzjxNGENDhAx6SGfoCqNnYdLb7mJHeaNKiOhTXvioewgjOl0o5F4twQt3UnvGwCVJHsBCEeB_pCl8kbL6lOMCjShqpNaVOY_9_GHoeHRZF9t1n5mgZRyl6tzxF5A3OfXH7O8Q3xwPVLRvkdxcFDHNDYk151rkUF99yvgKEQqsHsr-TaewVFvE_tnD_37&response_mode=form_post&nonce=639258398406938420.MDdlZWUwYzYtM2RjZS00Y2M3LWI2NjItOTliODI4ZDY5YzI2NzQyOGJiYmUtODRkNS00ZThhLWE3NzItNDg4MzJjNWQwNTE2&x-client-SKU=ID_NET472&x-client-ver=8.0.1.0');
  await page.goto('https://scrubdb-uat.trimont.com/');
  await page.goto('https://scrubdb-uat.trimont.com/ScrubDB/DashBoard');
  await page.goto('https://scrubdb-uat.trimont.com/Logout');
});