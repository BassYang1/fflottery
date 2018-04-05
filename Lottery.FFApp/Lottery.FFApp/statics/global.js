
/*	SWFObject v2.2 <http://code.google.com/p/swfobject/> 
is released under the MIT License <http://www.opensource.org/licenses/mit-license.php> 
*/
var swfobject = function () { var D = "undefined", r = "object", S = "Shockwave Flash", W = "ShockwaveFlash.ShockwaveFlash", q = "application/x-shockwave-flash", R = "SWFObjectExprInst", x = "onreadystatechange", O = window, j = document, t = navigator, T = false, U = [h], o = [], N = [], I = [], l, Q, E, B, J = false, a = false, n, G, m = true, M = function () { var aa = typeof j.getElementById != D && typeof j.getElementsByTagName != D && typeof j.createElement != D, ah = t.userAgent.toLowerCase(), Y = t.platform.toLowerCase(), ae = Y ? /win/.test(Y) : /win/.test(ah), ac = Y ? /mac/.test(Y) : /mac/.test(ah), af = /webkit/.test(ah) ? parseFloat(ah.replace(/^.*webkit\/(\d+(\.\d+)?).*$/, "$1")) : false, X = ! +"\v1", ag = [0, 0, 0], ab = null; if (typeof t.plugins != D && typeof t.plugins[S] == r) { ab = t.plugins[S].description; if (ab && !(typeof t.mimeTypes != D && t.mimeTypes[q] && !t.mimeTypes[q].enabledPlugin)) { T = true; X = false; ab = ab.replace(/^.*\s+(\S+\s+\S+$)/, "$1"); ag[0] = parseInt(ab.replace(/^(.*)\..*$/, "$1"), 10); ag[1] = parseInt(ab.replace(/^.*\.(.*)\s.*$/, "$1"), 10); ag[2] = /[a-zA-Z]/.test(ab) ? parseInt(ab.replace(/^.*[a-zA-Z]+(.*)$/, "$1"), 10) : 0 } } else { if (typeof O.ActiveXObject != D) { try { var ad = new ActiveXObject(W); if (ad) { ab = ad.GetVariable("$version"); if (ab) { X = true; ab = ab.split(" ")[1].split(","); ag = [parseInt(ab[0], 10), parseInt(ab[1], 10), parseInt(ab[2], 10)] } } } catch (Z) { } } } return { w3: aa, pv: ag, wk: af, ie: X, win: ae, mac: ac} } (), k = function () { if (!M.w3) { return } if ((typeof j.readyState != D && j.readyState == "complete") || (typeof j.readyState == D && (j.getElementsByTagName("body")[0] || j.body))) { f() } if (!J) { if (typeof j.addEventListener != D) { j.addEventListener("DOMContentLoaded", f, false) } if (M.ie && M.win) { j.attachEvent(x, function () { if (j.readyState == "complete") { j.detachEvent(x, arguments.callee); f() } }); if (O == top) { (function () { if (J) { return } try { j.documentElement.doScroll("left") } catch (X) { setTimeout(arguments.callee, 0); return } f() })() } } if (M.wk) { (function () { if (J) { return } if (!/loaded|complete/.test(j.readyState)) { setTimeout(arguments.callee, 0); return } f() })() } s(f) } } (); function f() { if (J) { return } try { var Z = j.getElementsByTagName("body")[0].appendChild(C("span")); Z.parentNode.removeChild(Z) } catch (aa) { return } J = true; var X = U.length; for (var Y = 0; Y < X; Y++) { U[Y]() } } function K(X) { if (J) { X() } else { U[U.length] = X } } function s(Y) { if (typeof O.addEventListener != D) { O.addEventListener("load", Y, false) } else { if (typeof j.addEventListener != D) { j.addEventListener("load", Y, false) } else { if (typeof O.attachEvent != D) { i(O, "onload", Y) } else { if (typeof O.onload == "function") { var X = O.onload; O.onload = function () { X(); Y() } } else { O.onload = Y } } } } } function h() { if (T) { V() } else { H() } } function V() { var X = j.getElementsByTagName("body")[0]; var aa = C(r); aa.setAttribute("type", q); var Z = X.appendChild(aa); if (Z) { var Y = 0; (function () { if (typeof Z.GetVariable != D) { var ab = Z.GetVariable("$version"); if (ab) { ab = ab.split(" ")[1].split(","); M.pv = [parseInt(ab[0], 10), parseInt(ab[1], 10), parseInt(ab[2], 10)] } } else { if (Y < 10) { Y++; setTimeout(arguments.callee, 10); return } } X.removeChild(aa); Z = null; H() })() } else { H() } } function H() { var ag = o.length; if (ag > 0) { for (var af = 0; af < ag; af++) { var Y = o[af].id; var ab = o[af].callbackFn; var aa = { success: false, id: Y }; if (M.pv[0] > 0) { var ae = c(Y); if (ae) { if (F(o[af].swfVersion) && !(M.wk && M.wk < 312)) { w(Y, true); if (ab) { aa.success = true; aa.ref = z(Y); ab(aa) } } else { if (o[af].expressInstall && A()) { var ai = {}; ai.data = o[af].expressInstall; ai.width = ae.getAttribute("width") || "0"; ai.height = ae.getAttribute("height") || "0"; if (ae.getAttribute("class")) { ai.styleclass = ae.getAttribute("class") } if (ae.getAttribute("align")) { ai.align = ae.getAttribute("align") } var ah = {}; var X = ae.getElementsByTagName("param"); var ac = X.length; for (var ad = 0; ad < ac; ad++) { if (X[ad].getAttribute("name").toLowerCase() != "movie") { ah[X[ad].getAttribute("name")] = X[ad].getAttribute("value") } } P(ai, ah, Y, ab) } else { p(ae); if (ab) { ab(aa) } } } } } else { w(Y, true); if (ab) { var Z = z(Y); if (Z && typeof Z.SetVariable != D) { aa.success = true; aa.ref = Z } ab(aa) } } } } } function z(aa) { var X = null; var Y = c(aa); if (Y && Y.nodeName == "OBJECT") { if (typeof Y.SetVariable != D) { X = Y } else { var Z = Y.getElementsByTagName(r)[0]; if (Z) { X = Z } } } return X } function A() { return !a && F("6.0.65") && (M.win || M.mac) && !(M.wk && M.wk < 312) } function P(aa, ab, X, Z) { a = true; E = Z || null; B = { success: false, id: X }; var ae = c(X); if (ae) { if (ae.nodeName == "OBJECT") { l = g(ae); Q = null } else { l = ae; Q = X } aa.id = R; if (typeof aa.width == D || (!/%$/.test(aa.width) && parseInt(aa.width, 10) < 310)) { aa.width = "310" } if (typeof aa.height == D || (!/%$/.test(aa.height) && parseInt(aa.height, 10) < 137)) { aa.height = "137" } j.title = j.title.slice(0, 47) + " - Flash Player Installation"; var ad = M.ie && M.win ? "ActiveX" : "PlugIn", ac = "MMredirectURL=" + O.location.toString().replace(/&/g, "%26") + "&MMplayerType=" + ad + "&MMdoctitle=" + j.title; if (typeof ab.flashvars != D) { ab.flashvars += "&" + ac } else { ab.flashvars = ac } if (M.ie && M.win && ae.readyState != 4) { var Y = C("div"); X += "SWFObjectNew"; Y.setAttribute("id", X); ae.parentNode.insertBefore(Y, ae); ae.style.display = "none"; (function () { if (ae.readyState == 4) { ae.parentNode.removeChild(ae) } else { setTimeout(arguments.callee, 10) } })() } u(aa, ab, X) } } function p(Y) { if (M.ie && M.win && Y.readyState != 4) { var X = C("div"); Y.parentNode.insertBefore(X, Y); X.parentNode.replaceChild(g(Y), X); Y.style.display = "none"; (function () { if (Y.readyState == 4) { Y.parentNode.removeChild(Y) } else { setTimeout(arguments.callee, 10) } })() } else { Y.parentNode.replaceChild(g(Y), Y) } } function g(ab) { var aa = C("div"); if (M.win && M.ie) { aa.innerHTML = ab.innerHTML } else { var Y = ab.getElementsByTagName(r)[0]; if (Y) { var ad = Y.childNodes; if (ad) { var X = ad.length; for (var Z = 0; Z < X; Z++) { if (!(ad[Z].nodeType == 1 && ad[Z].nodeName == "PARAM") && !(ad[Z].nodeType == 8)) { aa.appendChild(ad[Z].cloneNode(true)) } } } } } return aa } function u(ai, ag, Y) { var X, aa = c(Y); if (M.wk && M.wk < 312) { return X } if (aa) { if (typeof ai.id == D) { ai.id = Y } if (M.ie && M.win) { var ah = ""; for (var ae in ai) { if (ai[ae] != Object.prototype[ae]) { if (ae.toLowerCase() == "data") { ag.movie = ai[ae] } else { if (ae.toLowerCase() == "styleclass") { ah += ' class="' + ai[ae] + '"' } else { if (ae.toLowerCase() != "classid") { ah += " " + ae + '="' + ai[ae] + '"' } } } } } var af = ""; for (var ad in ag) { if (ag[ad] != Object.prototype[ad]) { af += '<param name="' + ad + '" value="' + ag[ad] + '" />' } } aa.outerHTML = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"' + ah + ">" + af + "</object>"; N[N.length] = ai.id; X = c(ai.id) } else { var Z = C(r); Z.setAttribute("type", q); for (var ac in ai) { if (ai[ac] != Object.prototype[ac]) { if (ac.toLowerCase() == "styleclass") { Z.setAttribute("class", ai[ac]) } else { if (ac.toLowerCase() != "classid") { Z.setAttribute(ac, ai[ac]) } } } } for (var ab in ag) { if (ag[ab] != Object.prototype[ab] && ab.toLowerCase() != "movie") { e(Z, ab, ag[ab]) } } aa.parentNode.replaceChild(Z, aa); X = Z } } return X } function e(Z, X, Y) { var aa = C("param"); aa.setAttribute("name", X); aa.setAttribute("value", Y); Z.appendChild(aa) } function y(Y) { var X = c(Y); if (X && X.nodeName == "OBJECT") { if (M.ie && M.win) { X.style.display = "none"; (function () { if (X.readyState == 4) { b(Y) } else { setTimeout(arguments.callee, 10) } })() } else { X.parentNode.removeChild(X) } } } function b(Z) { var Y = c(Z); if (Y) { for (var X in Y) { if (typeof Y[X] == "function") { Y[X] = null } } Y.parentNode.removeChild(Y) } } function c(Z) { var X = null; try { X = j.getElementById(Z) } catch (Y) { } return X } function C(X) { return j.createElement(X) } function i(Z, X, Y) { Z.attachEvent(X, Y); I[I.length] = [Z, X, Y] } function F(Z) { var Y = M.pv, X = Z.split("."); X[0] = parseInt(X[0], 10); X[1] = parseInt(X[1], 10) || 0; X[2] = parseInt(X[2], 10) || 0; return (Y[0] > X[0] || (Y[0] == X[0] && Y[1] > X[1]) || (Y[0] == X[0] && Y[1] == X[1] && Y[2] >= X[2])) ? true : false } function v(ac, Y, ad, ab) { if (M.ie && M.mac) { return } var aa = j.getElementsByTagName("head")[0]; if (!aa) { return } var X = (ad && typeof ad == "string") ? ad : "screen"; if (ab) { n = null; G = null } if (!n || G != X) { var Z = C("style"); Z.setAttribute("type", "text/css"); Z.setAttribute("media", X); n = aa.appendChild(Z); if (M.ie && M.win && typeof j.styleSheets != D && j.styleSheets.length > 0) { n = j.styleSheets[j.styleSheets.length - 1] } G = X } if (M.ie && M.win) { if (n && typeof n.addRule == r) { n.addRule(ac, Y) } } else { if (n && typeof j.createTextNode != D) { n.appendChild(j.createTextNode(ac + " {" + Y + "}")) } } } function w(Z, X) { if (!m) { return } var Y = X ? "visible" : "hidden"; if (J && c(Z)) { c(Z).style.visibility = Y } else { v("#" + Z, "visibility:" + Y) } } function L(Y) { var Z = /[\\\"<>\.;]/; var X = Z.exec(Y) != null; return X && typeof encodeURIComponent != D ? encodeURIComponent(Y) : Y } var d = function () { if (M.ie && M.win) { window.attachEvent("onunload", function () { var ac = I.length; for (var ab = 0; ab < ac; ab++) { I[ab][0].detachEvent(I[ab][1], I[ab][2]) } var Z = N.length; for (var aa = 0; aa < Z; aa++) { y(N[aa]) } for (var Y in M) { M[Y] = null } M = null; for (var X in swfobject) { swfobject[X] = null } swfobject = null }) } } (); return { registerObject: function (ab, X, aa, Z) { if (M.w3 && ab && X) { var Y = {}; Y.id = ab; Y.swfVersion = X; Y.expressInstall = aa; Y.callbackFn = Z; o[o.length] = Y; w(ab, false) } else { if (Z) { Z({ success: false, id: ab }) } } }, getObjectById: function (X) { if (M.w3) { return z(X) } }, embedSWF: function (ab, ah, ae, ag, Y, aa, Z, ad, af, ac) { var X = { success: false, id: ah }; if (M.w3 && !(M.wk && M.wk < 312) && ab && ah && ae && ag && Y) { w(ah, false); K(function () { ae += ""; ag += ""; var aj = {}; if (af && typeof af === r) { for (var al in af) { aj[al] = af[al] } } aj.data = ab; aj.width = ae; aj.height = ag; var am = {}; if (ad && typeof ad === r) { for (var ak in ad) { am[ak] = ad[ak] } } if (Z && typeof Z === r) { for (var ai in Z) { if (typeof am.flashvars != D) { am.flashvars += "&" + ai + "=" + Z[ai] } else { am.flashvars = ai + "=" + Z[ai] } } } if (F(Y)) { var an = u(aj, am, ah); if (aj.id == ah) { w(ah, true) } X.success = true; X.ref = an } else { if (aa && A()) { aj.data = aa; P(aj, am, ah, ac); return } else { w(ah, true) } } if (ac) { ac(X) } }) } else { if (ac) { ac(X) } } }, switchOffAutoHideShow: function () { m = false }, ua: M, getFlashPlayerVersion: function () { return { major: M.pv[0], minor: M.pv[1], release: M.pv[2]} }, hasFlashPlayerVersion: F, createSWF: function (Z, Y, X) { if (M.w3) { return u(Z, Y, X) } else { return undefined } }, showExpressInstall: function (Z, aa, X, Y) { if (M.w3 && A()) { P(Z, aa, X, Y) } }, removeSWF: function (X) { if (M.w3) { y(X) } }, createCSS: function (aa, Z, Y, X) { if (M.w3) { v(aa, Z, Y, X) } }, addDomLoadEvent: K, addLoadEvent: s, getQueryParamValue: function (aa) { var Z = j.location.search || j.location.hash; if (Z) { if (/\?/.test(Z)) { Z = Z.split("?")[1] } if (aa == null) { return L(Z) } var Y = Z.split("&"); for (var X = 0; X < Y.length; X++) { if (Y[X].substring(0, Y[X].indexOf("=")) == aa) { return L(Y[X].substring((Y[X].indexOf("=") + 1))) } } } return "" }, expressInstallCallback: function () { if (a) { var X = c(R); if (X && l) { X.parentNode.replaceChild(l, X); if (Q) { w(Q, true); if (M.ie && M.win) { l.style.display = "block" } } if (E) { E(B) } } a = false } } } } ();

//<!--网站参数begin

var site = new Object();
site.Dir = '/';
site.WebIsOpen = '0';
site.WebCloseSeason = '22222222222222222';
site.ZHIsOpen = '0';
site.RegIsOpen = '0';
site.BetIsOpen = '0';
site.CSUrl = 'http://www.hao123.com';
site.SignMinTotal = '1';
site.SignMaxTotal = '5';
site.SignNum = '100';
site.WarnTotal = '5000.00';
site.MaxBet = '1000000.00';
site.MaxWin = '200000.00';
site.MaxLevel = '13.10';
site.MinCharge = '50.00';
site.Points = '500';
site.PriceOutCheck = '30.00';
site.PriceOut = '100.00';
site.PriceOut2 = '50000.00';
site.PriceNum = '999';
site.PriceTime1 = '12:00';
site.PriceTime2 = '02:00';
site.BankTime = '6.00';
site.PriceOutPerson = '3';
site.ClientVersion = 'v1.0.0.6';
site.UpdateTime = '2014/11/4 12:46:18';
site.NewsUpdateTime = '2014/11/4 12:48:16';
site.AutoLottery = '0';
site.ProfitModel = '0';
site.ProfitMargin = '80';
site.AutoRanking = '0';
site.CookieDomain = '';
site.CookiePath = '/';
site.CookiePrev = 'LotteryUser';
site.CookieKeyCode = '';
site.Version = 'v1.0.0.6';


//-->网站参数end


/*=================以上信息请勿手工修改=================*/

//浏览器兼容访问DOM 
function thisMovie(movieName) {
    if (navigator.appName.indexOf("Microsoft") != -1) {
        return window[movieName];
    }
    else {
        return document[movieName];
    }
}
var _jcms_Host = function () {
    if (document.URL.substr(0, 7) == 'http://')
        return 'http://' + window.location.host;
    else
        return 'https://' + window.location.host;
}
var _jcms_DialogUrl = site.Dir + "statics/dialog/";


/*获得元素*/
function $i(el) {
    if (typeof el == 'string') return document.getElementById(el);
    else if (typeof el == 'object') return el;
    return null;
}
/*获得元素数组*/
function $A(els) { var _els = []; if (els instanceof Array) { for (var i = 0; i != els.length; i++) { _els[_els.length] = $i(els[i]); } } else if (typeof els == 'object' && typeof els['length'] != 'undefined' && els['length'] > 0) { for (var i = 0; i != els.length; i++) { _els[_els.length] = $i(els[i]); } } else { _els[0] = $i(els); } return _els; }

var Lottery = new Object();
var hexcase = 0; //1为大写
var chrsz = 8;

Lottery.MD5 = function (s) {
    if (s.length == 32)
        return s;
    return _jcms_binl2hex(_jcms_core_md5(_jcms_str2binl(s), s.length * chrsz));
}

function _jcms_core_md5(x, len) {

    x[len >> 5] |= 0x80 << ((len) % 32);
    x[(((len + 64) >>> 9) << 4) + 14] = len;

    var a = 1732584193;
    var b = -271733879;
    var c = -1732584194;
    var d = 271733878;

    for (var i = 0; i < x.length; i += 16) {
        var olda = a;
        var oldb = b;
        var oldc = c;
        var oldd = d;

        a = md5_ff(a, b, c, d, x[i + 0], 7, -680876936);
        d = md5_ff(d, a, b, c, x[i + 1], 12, -389564586);
        c = md5_ff(c, d, a, b, x[i + 2], 17, 606105819);
        b = md5_ff(b, c, d, a, x[i + 3], 22, -1044525330);
        a = md5_ff(a, b, c, d, x[i + 4], 7, -176418897);
        d = md5_ff(d, a, b, c, x[i + 5], 12, 1200080426);
        c = md5_ff(c, d, a, b, x[i + 6], 17, -1473231341);
        b = md5_ff(b, c, d, a, x[i + 7], 22, -45705983);
        a = md5_ff(a, b, c, d, x[i + 8], 7, 1770035416);
        d = md5_ff(d, a, b, c, x[i + 9], 12, -1958414417);
        c = md5_ff(c, d, a, b, x[i + 10], 17, -42063);
        b = md5_ff(b, c, d, a, x[i + 11], 22, -1990404162);
        a = md5_ff(a, b, c, d, x[i + 12], 7, 1804603682);
        d = md5_ff(d, a, b, c, x[i + 13], 12, -40341101);
        c = md5_ff(c, d, a, b, x[i + 14], 17, -1502002290);
        b = md5_ff(b, c, d, a, x[i + 15], 22, 1236535329);

        a = md5_gg(a, b, c, d, x[i + 1], 5, -165796510);
        d = md5_gg(d, a, b, c, x[i + 6], 9, -1069501632);
        c = md5_gg(c, d, a, b, x[i + 11], 14, 643717713);
        b = md5_gg(b, c, d, a, x[i + 0], 20, -373897302);
        a = md5_gg(a, b, c, d, x[i + 5], 5, -701558691);
        d = md5_gg(d, a, b, c, x[i + 10], 9, 38016083);
        c = md5_gg(c, d, a, b, x[i + 15], 14, -660478335);
        b = md5_gg(b, c, d, a, x[i + 4], 20, -405537848);
        a = md5_gg(a, b, c, d, x[i + 9], 5, 568446438);
        d = md5_gg(d, a, b, c, x[i + 14], 9, -1019803690);
        c = md5_gg(c, d, a, b, x[i + 3], 14, -187363961);
        b = md5_gg(b, c, d, a, x[i + 8], 20, 1163531501);
        a = md5_gg(a, b, c, d, x[i + 13], 5, -1444681467);
        d = md5_gg(d, a, b, c, x[i + 2], 9, -51403784);
        c = md5_gg(c, d, a, b, x[i + 7], 14, 1735328473);
        b = md5_gg(b, c, d, a, x[i + 12], 20, -1926607734);

        a = md5_hh(a, b, c, d, x[i + 5], 4, -378558);
        d = md5_hh(d, a, b, c, x[i + 8], 11, -2022574463);
        c = md5_hh(c, d, a, b, x[i + 11], 16, 1839030562);
        b = md5_hh(b, c, d, a, x[i + 14], 23, -35309556);
        a = md5_hh(a, b, c, d, x[i + 1], 4, -1530992060);
        d = md5_hh(d, a, b, c, x[i + 4], 11, 1272893353);
        c = md5_hh(c, d, a, b, x[i + 7], 16, -155497632);
        b = md5_hh(b, c, d, a, x[i + 10], 23, -1094730640);
        a = md5_hh(a, b, c, d, x[i + 13], 4, 681279174);
        d = md5_hh(d, a, b, c, x[i + 0], 11, -358537222);
        c = md5_hh(c, d, a, b, x[i + 3], 16, -722521979);
        b = md5_hh(b, c, d, a, x[i + 6], 23, 76029189);
        a = md5_hh(a, b, c, d, x[i + 9], 4, -640364487);
        d = md5_hh(d, a, b, c, x[i + 12], 11, -421815835);
        c = md5_hh(c, d, a, b, x[i + 15], 16, 530742520);
        b = md5_hh(b, c, d, a, x[i + 2], 23, -995338651);

        a = md5_ii(a, b, c, d, x[i + 0], 6, -198630844);
        d = md5_ii(d, a, b, c, x[i + 7], 10, 1126891415);
        c = md5_ii(c, d, a, b, x[i + 14], 15, -1416354905);
        b = md5_ii(b, c, d, a, x[i + 5], 21, -57434055);
        a = md5_ii(a, b, c, d, x[i + 12], 6, 1700485571);
        d = md5_ii(d, a, b, c, x[i + 3], 10, -1894986606);
        c = md5_ii(c, d, a, b, x[i + 10], 15, -1051523);
        b = md5_ii(b, c, d, a, x[i + 1], 21, -2054922799);
        a = md5_ii(a, b, c, d, x[i + 8], 6, 1873313359);
        d = md5_ii(d, a, b, c, x[i + 15], 10, -30611744);
        c = md5_ii(c, d, a, b, x[i + 6], 15, -1560198380);
        b = md5_ii(b, c, d, a, x[i + 13], 21, 1309151649);
        a = md5_ii(a, b, c, d, x[i + 4], 6, -145523070);
        d = md5_ii(d, a, b, c, x[i + 11], 10, -1120210379);
        c = md5_ii(c, d, a, b, x[i + 2], 15, 718787259);
        b = md5_ii(b, c, d, a, x[i + 9], 21, -343485551);

        a = _jcms_safe_add(a, olda);
        b = _jcms_safe_add(b, oldb);
        c = _jcms_safe_add(c, oldc);
        d = _jcms_safe_add(d, oldd);
    }
    return Array(a, b, c, d);

}


function _jcms_md5_cmn(q, a, b, x, s, t) {
    return _jcms_safe_add(_jcms_bit_rol(_jcms_safe_add(_jcms_safe_add(a, q), _jcms_safe_add(x, t)), s), b);
}
function md5_ff(a, b, c, d, x, s, t) {
    return _jcms_md5_cmn((b & c) | ((~b) & d), a, b, x, s, t);
}
function md5_gg(a, b, c, d, x, s, t) {
    return _jcms_md5_cmn((b & d) | (c & (~d)), a, b, x, s, t);
}
function md5_hh(a, b, c, d, x, s, t) {
    return _jcms_md5_cmn(b ^ c ^ d, a, b, x, s, t);
}
function md5_ii(a, b, c, d, x, s, t) {
    return _jcms_md5_cmn(c ^ (b | (~d)), a, b, x, s, t);
}

function _jcms_core_hmac_md5(key, data) {
    var bkey = _jcms_str2binl(key);
    if (bkey.length > 16) bkey = _jcms_core_md5(bkey, key.length * chrsz);

    var ipad = Array(16), ōpad = Array(16);
    for (var i = 0; i < 16; i++) {
        ipad[i] = bkey[i] ^ 0x36363636;
        opad[i] = bkey[i] ^ 0x5C5C5C5C;
    }

    var hash = _jcms_core_md5(ipad.concat(_jcms_str2binl(data)), 512 + data.length * chrsz);
    return _jcms_core_md5(opad.concat(hash), 512 + 128);
}


function _jcms_safe_add(x, y) {
    var lsw = (x & 0xFFFF) + (y & 0xFFFF);
    var msw = (x >> 16) + (y >> 16) + (lsw >> 16);
    return (msw << 16) | (lsw & 0xFFFF);
}

function _jcms_bit_rol(num, cnt) {
    return (num << cnt) | (num >>> (32 - cnt));
}


function _jcms_str2binl(str) {
    var bin = Array();
    var mask = (1 << chrsz) - 1;
    for (var i = 0; i < str.length * chrsz; i += chrsz)
        bin[i >> 5] |= (str.charCodeAt(i / chrsz) & mask) << (i % 32);
    return bin;
}



function _jcms_binl2hex(binarray) {
    var hex_tab = hexcase ? "0123456789ABCDEF" : "0123456789abcdef";
    var str = "";
    for (var i = 0; i < binarray.length * 4; i++) {
        str += hex_tab.charAt((binarray[i >> 2] >> ((i % 4) * 8 + 4)) & 0xF) +
           hex_tab.charAt((binarray[i >> 2] >> ((i % 4) * 8)) & 0xF);
    }
    return str;
}

Lottery.Cookie = {//
    set: function (name, value, expires, path, domain) {
        if (typeof expires == "undefined") {
            expires = new Date(new Date().getTime() + 24 * 3600 * 100);
        }
        document.cookie = name + "=" + _jcms_UrlEncode(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "; path=/") + ((domain != null && domain.length > 0) ? ";domain=" + domain : "");
    },
    get: function (name, subname) {
        var re = new RegExp((subname ? name + "=(.*?&)*?" + subname + "=(.*?)(&|;)" : name + "=([^;]*)(;|$)"), "i");
        return _jcms_UrlDecode(re.test(document.cookie) ? (subname ? RegExp["$2"] : RegExp["$1"]) : "");
    },
    clear: function (name, path, domain) {
        if (this.get(name)) {
            document.cookie = name + "=" + ((path) ? "; path=" + path : "; path=/") + ((domain) ? "; domain=" + domain : "") + ";expires=Fri, 02-Jan-1970 00:00:00 GMT";
        }
    }
};
//追加/删除事件
Lottery.Event = {
    add: function (obj, evType, fn) {
        if (obj.addEventListener) { obj.addEventListener(evType, fn, false); return true; }
        else if (obj.attachEvent) { var r = obj.attachEvent("on" + evType, fn); return r; }
        else { return false; }
    },
    remove: function (obj, evType, fn, useCapture) {
        if (obj.removeEventListener) { obj.removeEventListener(evType, fn, useCapture); return true; }
        else if (obj.detachEvent) { var r = obj.detachEvent("on" + evType, fn); return r; }
        else { alert("Handler could not be removed"); }
    }
};
//追加onload事件
Lottery.addOnloadEvent = function (fnc) {
    if (typeof window.addEventListener != "undefined")
        window.addEventListener("load", fnc, false);
    else if (typeof window.attachEvent != "undefined") {
        window.attachEvent("onload", fnc);
    }
    else {
        if (window.onload != null) {
            var oldOnload = window.onload;
            window.onload = function (e) {
                oldOnload(e);
                window[fnc]();
            };
        } else
            window.onload = fnc;
    }
};
Lottery.isFunction = function (variable) { return (typeof (variable) == 'function' ? true : false); };
Lottery.isUndefined = function (variable) { return (typeof (variable) == 'undefined' ? true : false); };
/*字节长度*/
Lottery.Length = function (variable) {
    var len = 0;
    var val = variable;
    for (var i = 0; i < val.length; i++) {
        if (val.charCodeAt(i) >= 0x4e00 && val.charCodeAt(i) <= 0x9fa5) {
            len += 2;
        } else {
            len++;
        }
    }
    return len;
};
Lottery.Eval = function (data) {
    Lottery.Loading.hide();
    try {
        eval(data);
    }
    catch (e) {
        alert(data);
    }
};
/////////////////////////////
//弹出消息框
/////////////////////////////
Lottery.Message = function (errstr, success, returnFunc) {
    var MSG = $.message;
    MSG.lays(200, 24, 2);
    //MSG.anim('fade', 'slow');
    MSG.anim('fade', 'slow', site.Dir + 'libs/jquery.messager/');
    if (returnFunc) MSG.doafter(returnFunc);
    MSG.show(success, errstr, 1500);
    new _jcms_Dialog().reset(); //消息显示完毕完再执行
};
/////////////////////////////
//弹出提示框
/////////////////////////////
Lottery.Alert = function (errstr, success, returnFunc) {
    var oDialog = new _jcms_Dialog('2', '', 360, 180, success, true);
    oDialog.init();
    oDialog.event(errstr, '');
    if (returnFunc == null)
        oDialog.button('dialogSubmit', '');
    else
        oDialog.button('dialogSubmit', returnFunc);
};
/////////////////////////////
//弹出确认框
//例如:
//1、Lottery.Confirm("是否操作", act, null) //函数不加()
//2、Lottery.Confirm("是否操作", "alert('yes')", "alert('no')")
/////////////////////////////
Lottery.Confirm = function (errstr, returnSubmitFunc, returnCancelFunc) {
    var oDialog = new _jcms_Dialog('2', '', 360, 180, null, true);
    oDialog.init();
    oDialog.event(errstr, '');
    oDialog.button('dialogSubmit', returnSubmitFunc);
    if (returnCancelFunc == null)
        oDialog.button('dialogCancel', '');
    else
        oDialog.button('dialogCancel', returnCancelFunc);
};
/////////////////////////////
//弹出模拟窗口
/////////////////////////////
Lottery.Popup = {
    show: function (url, width, height, showCloseBox, showTitle, returnFunc) {
        new _jcms_Dialog().reset();
        if (showTitle == null) showTitle = "&nbsp;";
        var oDialog = new _jcms_Dialog('2', showTitle, width, height, null, showCloseBox);
        if (url.indexOf("?") == -1)
            oDialog.open(url + "?windowCode=" + (new Date().getTime()), returnFunc, "auto");
        else
            oDialog.open(url + "&windowCode=" + (new Date().getTime()), returnFunc, "auto");
    },
    hide: function (callReturnFunc) {
        new _jcms_Dialog().reset(callReturnFunc);
    }
};
/////////////////////////////
//弹出加载层
/////////////////////////////
Lottery.Loading = {
    show: function (msgstr, width, height) {
        if (width == null) width = 280;
        if (height == null) height = 100;
        var oDialog = new _jcms_Dialog('0', '友情提示', width, height, null, false);
        oDialog.init(true);
        oDialog.html("<div style='text-align:center;padding-top:20px;'>" + msgstr + "<br /><br /><img src='" + _jcms_DialogUrl + "images/loading.gif' align='absmiddle'></div>");
    },
    hide: function (callReturnFunc) {
        new _jcms_Dialog().reset(callReturnFunc);
    }
};
Lottery.Event.add(window, "load", _jcms_OperatorPlus);
Lottery.Event.add(window, "scroll", _jcms_OperatorPlus);
Lottery.Event.add(window, "resize", _jcms_OperatorPlus);


Lottery.Core = {
    WriteUserInfo: function () {
        $.ajax({
            type: "get",
            dataType: "json",
            url: _jcms_Host() + site.Dir + "ajax/user.aspx?oper=ajaxLoginbar&clienttime=" + Math.random(),
            error: function (XmlHttpRequest, textStatus, errorThrown) { alert(XmlHttpRequest.responseText); },
            success: function (d) {
                if (d.result == "1") {//成功登录
                    var _pmBar = '', _cartBar = '';
                    if (d.newmessage > 0)
                        _pmBar = '<li class="ico pm1"><a href="' + site.Dir + 'user/message_list.aspx" title="' + d.newmessage + '条未读消息" target="_blank">短消息<span style="color:red;">(' + d.newmessage + ')</span></a></li>';
                    else
                        _pmBar = '<li class="ico pm0"><a href="' + site.Dir + 'user/message_list.aspx" title="0条未读消息" target="_blank">短消息</a></li>';
                    if (d.newcart > 0)
                        _cartBar = '<li class="ico cart1"><a href="' + site.Dir + 'user/maimai_cart.aspx" title="' + d.newcart + '件待购商品" target="_blank">购物车<span style="color:red;">(' + d.newcart + ')</span></a></li>';
                    else
                        _cartBar = '<li class="ico cart0"><a href="' + site.Dir + 'user/maimai_cart.aspx" title="0件待购商品" target="_blank">购物车</a></li>';
                    var _ajaxLoginbar = '\
						<ul>\
							<li class="ico user"><a href="' + site.Dir + 'user/default.aspx" title="进入个人中心" target="_blank"><b>' + d.username + '</b></a></li>\
							<li><a href="' + site.Dir + 'passport/logout.aspx?userkey=' + d.userkey + '">[退出]</a></li>\
							<li class="ico points"><a href="' + site.Dir + 'user/bobi_buypoints.aspx" target="_blank" title="当前账号剩余' + d.points + '元，点击马上充值">账号</a></li>' + _pmBar + _cartBar + '\
						</ul>\
					';
                    $("#user_status").html(_ajaxLoginbar);
                }
                $("#user_status").show();

            }
        });
    },
    ShowWeather: function (obj) {
        $.ajax({
            type: "get",
            dataType: "json",
            data: "clienttime=" + Math.random(),
            url: _jcms_Host() + site.Dir + "plus/weather/json.aspx",
            success: function (d) {
                $('#' + obj).html(d.cityname + " <br /><span style='vertical-align:text-bottom;'><img alt='" + d.weather + "' title='" + d.weather + "' src='" + site.Dir + "plus/weather/icon/" + d.img + ".gif' /></span> " + d.temperature);
            }
        });
    },
    AddDigg: function (cType, id) {
        $.ajax({
            type: "get",
            dataType: "json",
            data: "oper=ajaxDiggAdd&id=" + id + "&cType=" + cType + "&clienttime=" + Math.random(),
            url: _jcms_Host() + site.Dir + "digg/ajax.aspx",
            error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { alert(XmlHttpRequest.responseText); } },
            success: function (d) {
                $("#ajaxDigg_" + id).text(d.returnval);
                $("#DiggSpan" + id).html("谢谢支持");
            }
        });
    }

};

////////////////////////////////////////////////////////////////////////////////////////////
//标题栏跑马灯
////////////////////////////////////////////////////////////////////////////////////////////
var _jcms_ScrollTitle__Oldtitle = top.document.title;
var _jcms_ScrollTitle__i = 0;
var _jcms_ScrollTitle__Speed = 200;
var _jcms_ScrollTitle__Timer = function (message) {
    if (_jcms_ScrollTitle__i == message.length) {
        top.document.title = _jcms_ScrollTitle__Oldtitle;
        _jcms_ScrollTitle__i = 0;
        return;
    }
    else {
        top.document.title = message.substring(_jcms_ScrollTitle__i);
        _jcms_ScrollTitle__i++;
        setTimeout("_jcms_ScrollTitle__Timer('" + message + "')", _jcms_ScrollTitle__Speed);
    }
}

////////////////////////////////////////////////////////////////////////////////////////////

var _jcms_HideSelects = false;
var _jcms_DialogIsShown = false;
var _jcms_WindowMask = null;
////////////////////////////////////////////////////////////////////////////////////////////
//以下为弹出窗口的类
////////////////////////////////////////////////////////////////////////////////////////////
function _jcms_Dialog(styletype, title, width, height, iswhat, showCloseBox) {
    //半透明边框宽度
    var shadowBorderBoth = 0;
    var oWidth = width;
    var oHeight = height;
    if (oWidth == -1 || oWidth > _jcms_GetViewportWidth() - 15) {
        oWidth = _jcms_GetViewportWidth() - 15;
        shadowBorderBoth = 0;
    }
    if (oWidth < -1) {
        oWidth = _jcms_GetViewportWidth() + oWidth;
        shadowBorderBoth = 0;
    }
    if (oHeight == -1 || oHeight > _jcms_GetViewportHeight() - 44) {
        oHeight = _jcms_GetViewportHeight() - 44;
        shadowBorderBoth = 0;
    }
    if (oHeight < -1) {
        oHeight = _jcms_GetViewportHeight() + oHeight;
        shadowBorderBoth = 0;
    }
    var sTitle = "友情提示";
    if (iswhat == "0")
        sTitle = "错误提示";
    else if (iswhat == "1")
        sTitle = "成功提示";
    else
        if (title != '') sTitle = title;
    var src = "";
    var path = _jcms_DialogUrl + styletype + "/";
    var gReturnFunc;
    var gReturnVal = null;
    var sButtonFunc = '<input id="dialogSubmit" class="dialogSubmit' + styletype + '" type="button" value="确 认" onclick="new _jcms_Dialog().reset();" /> <input id="dialogCancel" class="dialogCancel' + styletype + '" type="button" value="取 消" onclick="new _jcms_Dialog().reset();" />';
    var sClose = '';
    if (showCloseBox == null || showCloseBox == true)
        sClose = '<img alt="关闭" style="cursor:pointer;" id="dialogBoxClose" onclick="new _jcms_Dialog().reset();" src="' + path + 'dialogCloseOut.png" border="0" onmouseover="this.src=\'' + path + 'dialogCloseOver.png\';" onmouseout="this.src=\'' + path + 'dialogCloseOut.png\';" align="absmiddle" />';
    var sSuccess = '';
    if (iswhat != null)
        sSuccess = '<td width="80" align="center" valign="middle"><img id="dialogBoxFace" class="dialogBoxFace' + styletype + '" src="' + path + iswhat + '.gif" valign="absmiddle" /></td>';
    else
        sSuccess = '<td width="80" align="center" valign="middle"><img id="dialogBoxFace" class="dialogBoxFace' + styletype + '" src="' + path + '0.gif" valign="absmiddle" /></td>';
    var sBody = '\
        <table id="dialogBodyBox" class="dialogBodyBox' + styletype + '" border="0" align="center" cellpadding="0" cellspacing="0" width="100%" height="100%" >\
            <tr height="' + (oHeight - 60) + '">\
                <td width="10"></td>' + sSuccess + '<td id="dialogMsg" class="dialogMsg' + styletype + '"></td>\
                <td width="10"></td>\
            </tr>\
            <tr height="30"><td id="dialogFunc" class="dialogFunc' + styletype + '" colspan="4">' + sButtonFunc + '</td></tr>\
        </table>\
    ';
    var sBox = '\
        <div style="display:none;" id="dialogBox" class="dialogBox' + styletype + '">\
            <div id="dialogTitleDiv" class="dialogTitleDiv' + styletype + '" style="width:' + oWidth + 'px;">\
                <span id="dialogBoxTitle" class="dialogBoxTitle' + styletype + '">' + sTitle + '</span>\
                <span id="dialogBoxClose" class="dialogBoxClose' + styletype + '">' + sClose + '</span>\
            </div>\
            <div id="dialogHeight" style="width:' + oWidth + 'px;height:' + oHeight + 'px;">\
                <div id="dialogBody" class="dialogBody' + styletype + '" style="height:' + oHeight + 'px;">' + sBody + '</div>\
            </div>\
        </div>\
        <div id="dialogBoxShadow" style="display:none;"></div>\
    ';
    this.init = function (_showTitleBar) {
        document.body.oncontextmenu = function () { return false; };
        document.body.onselectstart = function () { return false; };
        document.body.ondragstart = function () { return false; };
        document.body.onsource = function () { return false; };
        document.body.style.overflow = 'hidden'; //屏蔽滚动条
        $i('dialogFrame') ? $i('dialogFrame').src = '' : function () { };
        $i('dialogCase') ? $i('dialogCase').parentNode.removeChild($i('dialogCase')) : function () { };
        $i('windowMask') ? $i('windowMask').parentNode.removeChild($i('windowMask')) : function () { };
        var oDiv = document.createElement('span');
        oDiv.id = "dialogCase";
        oDiv.innerHTML = sBox;
        document.body.appendChild(oDiv);
        var oMask = document.createElement('div');
        oMask.id = 'windowMask';
        document.body.appendChild(oMask);
        _jcms_WindowMask = $i("windowMask");
        _jcms_WindowMask.style.display = "block";
        var brsVersion = parseInt(window.navigator.appVersion.charAt(0), 10);
        if (brsVersion <= 6 && window.navigator.userAgent.indexOf("MSIE") > -1) {
            _jcms_HideSelects = true;
        }
        if (_jcms_HideSelects == true) {
            HideSelectBoxes();
        }
        if (_showTitleBar == true || _showTitleBar == null)
            $i("dialogTitleDiv").style.display = "block";
        else
            $i("dialogTitleDiv").style.display = "none";
        _jcms_OperatorPlus();
    }
    //this.show = function(){$i('dialogBox') ? function(){} : this.init();_jcms_DialogIsShown=true;this.middle('dialogBox');}
    this.show = function () { $i('dialogBox') ? function () { } : this.init(); _jcms_DialogIsShown = true; this.middle('dialogBox'); this.shadow(); this.middle('dialogBoxShadow'); _jcms_OperatorPlus(); }
    this.html = function (_sHtml) {
        this.show();
        $i('dialogBody').innerHTML = _sHtml;
    }
    this.button = function (_sId, _sFuc) {
        if ($i(_sId)) {
            $i(_sId).style.display = '';
            if ($i(_sId).addEventListener) {
                if ($i(_sId).act) { $i(_sId).removeEventListener('click', function () { eval($i(_sId).act); }, false); }
                $i(_sId).act = _sFuc;
                $i(_sId).addEventListener('click', function () { eval(_sFuc); this.reset(); }, false);
            } else {
                if ($i(_sId).act) { $i(_sId).detachEvent('onclick', function () { eval($i(_sId).act); }); }
                $i(_sId).act = _sFuc;
                $i(_sId).attachEvent('onclick', function () { eval(_sFuc); });
            }
        }
    }
    this.shadow = function () {
        if (shadowBorderBoth > 0) {
            var oShadow = $i('dialogBoxShadow');
            var oDialogDiv = $i('dialogBox');
            oShadow.style.position = "absolute";
            oShadow.style.background = "#000";
            oShadow.style.display = "";
            oShadow.style.opacity = "0.25";
            oShadow.style.filter = "alpha(opacity=25)";
            oShadow.style.width = (oDialogDiv.offsetWidth + shadowBorderBoth) + "px";
            oShadow.style.height = (oDialogDiv.offsetHeight + shadowBorderBoth) + "px";
        }
    }
    this.open = function (_sUrl, _returnFunc, _sMode) {
        this.show();
        gReturnFunc = _returnFunc;
        //if(!_sMode || _sMode == "no" || _sMode == "yes"){
        $i("dialogBody").innerHTML = "<iframe id='dialogFrame' name='dialogFrame' src='" + site.Dir + "/statics/include/loading.html' width='" + oWidth + "' height='" + oHeight + "' frameborder='no' border='0' marginwidth='0' marginheight='0' scrolling='" + _sMode + "'></iframe>";
        //$i("dialogFrame").src = _sUrl;
        setTimeout("dialogFrame.location.href='" + _sUrl + "';", 100); //为了避免一直加载
        //}
    }
    this.reset = function (callReturnFunc) { $i('dialogCase') ? this.dispose(callReturnFunc) : function () { }; }
    this.dispose = function (callReturnFunc) {
        _jcms_DialogIsShown = false;
        document.body.oncontextmenu = function () { return true; };
        document.body.onselectstart = function () { return true; };
        document.body.ondragstart = function () { return true; };
        document.body.onsource = function () { return true; };
        document.body.style.overflow = ''; //恢复滚动条
        $i('dialogFrame') ? $i('dialogFrame').src = '' : function () { };
        $i('dialogCase').parentNode.removeChild($i('dialogCase'));
        $i('windowMask').parentNode.removeChild($i('windowMask'));
        _jcms_WindowMask = null;
        if (callReturnFunc == true && gReturnFunc != null) {
            gReturnVal = window.dialogFrame.returnVal;
            window.setTimeout('gReturnFunc(gReturnVal);', 1);
        }
        else if (callReturnFunc != null) {
            eval(callReturnFunc);
        }
        if (_jcms_HideSelects == true) {
            ShowSelectBoxes();
            _jcms_HideSelects = false;
        }
        //$i('dialogBoxShadow').style.display = "none";
    }
    this.event = function (_sMsg, _sSubmit, _sCancel, _sClose) {
        this.show();
        $i('dialogFunc').innerHTML = sButtonFunc;
        $i('dialogBoxClose').innerHTML = sClose;
        $i('dialogBodyBox') == null ? $i('dialogBody').innerHTML = sBody : function () { };
        $i('dialogMsg') ? $i('dialogMsg').innerHTML = _sMsg : function () { };
        _sSubmit ? this.button('dialogSubmit', _sSubmit) | $i('dialogSubmit').focus() : $i('dialogSubmit').style.display = "none";
        _sCancel ? this.button('dialogCancel', _sCancel) : $i('dialogCancel').style.display = "none";
        _sClose ? this.button('dialogBoxClose', _sClose) : function () { };
    }
    this.set = function (_oAttr, _sVal) {
        var oDialogDiv = $i('dialogBox');
        var oHeight = $i('dialogHeight');
        if (_sVal != '') {
            switch (_oAttr) {
                case 'title':
                    $i('dialogBoxTitle').innerHTML = _sVal;
                    title = _sVal;
                    break;
                case 'width':
                    oDialogDiv.style.width = _sVal;
                    width = _sVal;
                    this.middle('dialogBox');
                    this.shadow();
                    this.middle('dialogBoxShadow');
                    _jcms_OperatorPlus();
                    break;
                case 'height':
                    oHeight.style.height = _sVal;
                    height = _sVal;
                    this.middle('dialogBox');
                    this.shadow();
                    this.middle('dialogBoxShadow');
                    _jcms_OperatorPlus();
                    break;
                case 'src':
                    if (parseInt(_sVal) > 0) {
                        $i('dialogBoxFace') ? $i('dialogBoxFace').src = path + _sVal + '.png' : function () { };
                    } else {
                        $i('dialogBoxFace') ? $i('dialogBoxFace').src = _sVal : function () { };
                    }
                    src = _sVal;
                    break;
                case 'url':
                    this.open(_sVal);
                    break;
            }
        }
    }
    this.middle = function (_sId) {
        var theWidth;
        var theHeight;
        if (document.documentElement && document.documentElement.clientWidth) {
            theWidth = document.documentElement.clientWidth + document.documentElement.scrollLeft * 2;
            theHeight = document.documentElement.clientHeight + document.documentElement.scrollTop * 2;
        } else if (document.body) {
            theWidth = document.body.clientWidth;
            theHeight = document.body.clientHeight;
        } else if (window.innerWidth) {
            theWidth = window.innerWidth;
            theHeight = window.innerHeight;
        }
        $i(_sId).style.display = '';
        $i(_sId).style.position = "absolute";
        $i(_sId).style.left = (theWidth / 2) - ($i(_sId).offsetWidth / 2) + "px";
        if (document.all || $i("user_page_top")) {
            $i(_sId).style.top = (theHeight / 2 + document.body.scrollTop) - ($i(_sId).offsetHeight / 2) + "px";
        } else {
            var sClientHeight = parent ? parent.document.body.clientHeight : document.body.clientHeight;
            var sScrollTop = parent ? parent.document.body.scrollTop : document.body.scrollTop;
            var sTop = -80 + (sClientHeight / 2 + sScrollTop) - ($i(_sId).offsetHeight / 2);
            $i(_sId).style.top = (theHeight / 2 + document.body.scrollTop) - ($i(_sId).offsetHeight / 2) + "px";
        }
    }
    BtnOver = function (obj, path) { obj.style.backgroundImage = "url(" + path + "button2.gif)"; }
    BtnOut = function (obj, path) { obj.style.backgroundImage = "url(" + path + "button1.gif)"; }
    ShowSelectBoxes = function () { var x = document.getElementsByTagName("SELECT"); for (i = 0; x && i < x.length; i++) { x[i].style.visibility = "visible"; } }
    HideSelectBoxes = function () { var x = document.getElementsByTagName("SELECT"); for (i = 0; x && i < x.length; i++) { x[i].style.visibility = "hidden"; } }

}
///////////////////////////////////////////////////////////////////////////
function _jcms_OperatorPlus() {
    if (_jcms_DialogIsShown == true) {
        var oDialogDiv = $i("dialogBox");
        var oShadow = $i("dialogBoxShadow");
        var oWidth = oDialogDiv.offsetWidth;
        var oHeight = oDialogDiv.offsetHeight;
        var theBody = document.getElementsByTagName("BODY")[0];
        var scTop = parseInt(_jcms_GetScrollTop(), 10);
        var scLeft = parseInt(theBody.scrollLeft, 10);
        var fullHeight = _jcms_GetViewportHeight();
        var fullWidth = _jcms_GetViewportWidth();
        oDialogDiv.style.top = (scTop + ((fullHeight - oHeight) / 2)) + "px";
        oDialogDiv.style.left = (scLeft + ((fullWidth - oWidth) / 2)) + "px";
        oShadow.style.top = (scTop + ((fullHeight - oShadow.offsetHeight) / 2)) + "px";
        oShadow.style.left = (scLeft + ((fullWidth - oShadow.offsetWidth) / 2)) + "px";
        if (_jcms_WindowMask != null) {
            var popHeight = theBody.scrollHeight;
            var popWidth = theBody.scrollWidth;
            if (fullHeight > theBody.scrollHeight) popHeight = fullHeight;
            if (fullWidth > theBody.scrollWidth) popWidth = fullWidth;
            _jcms_WindowMask.style.height = popHeight + "px";
            _jcms_WindowMask.style.width = popWidth + "px";
        }
    }
}
function _jcms_GetViewportHeight() {
    if (window.innerHeight != window.undefined)//FF
    {
        return window.innerHeight;
    }
    if (document.compatMode == 'CSS1Compat')//IE
    {
        return document.documentElement.clientHeight;
    }
    if (document.body)//other
    {
        return document.body.clientHeight;
    }
    return window.undefined;
}
function _jcms_GetViewportWidth() {
    var offset = 17;
    var width = null;
    if (window.innerWidth != window.undefined)//FF
    {
        //return window.innerWidth-offset; 
        return window.innerWidth;
    }
    if (document.compatMode == 'CSS1Compat')//IE
    {
        return document.documentElement.clientWidth;
    }
    if (document.body)//other
    {
        return document.body.clientWidth;
    }
    return window.undefined;
}
function _jcms_GetScrollTop() {
    if (self.pageYOffset) { return self.pageYOffset; }
    else if (document.documentElement && document.documentElement.scrollTop) { return document.documentElement.scrollTop; }
    else if (document.body) { return document.body.scrollTop; }
}
function _jcms_GetScrollLeft() {
    if (self.pageXOffset) { return self.pageXOffset; }
    else if (document.documentElement && document.documentElement.scrollLeft) { return document.documentElement.scrollLeft; }
    else if (document.body) { return document.body.scrollLeft; }
}
function _jcms_SetDialogTitle() {
    //var _title = window.document.title + ' - 按Esc键可关闭窗口';
    var _title = window.document.title;
    try {
        $i('dialogBoxTitle').innerHTML = _title;
    }
    catch (e) {
        try {
            parent.$i('dialogBoxTitle').innerHTML = _title;
        }
        catch (e) {
        }
    }
    $(document).keyup(function (e) {
        var key = window.event ? e.keyCode : e.which;
        if (key == 27) {
            parent.Lottery.Popup.hide();
        }
    });
}
function _jcms_SetDialogSize(w, h) {
    try {
        if (w > 0) $i('dialogBox').style.width = w;
        if (h > 0) $i('dialogHeight').style.height = h;
        _jcms_OperatorPlus();
    }
    catch (e) {
        try {
            if (w > 0) parent.$i('dialogBox').style.width = w;
            if (h > 0) parent.$i('dialogHeight').style.height = h;
            parent._jcms_OperatorPlus();
        }
        catch (e) {
        }
    }
}
/* Url编码 */
function _jcms_UrlEncode(unzipStr) {
    var zipstr = "";
    var strSpecial = "!\"#$%&'()*+,/:;<=>?[]^`{|}~%";
    var tt = "";
    for (var i = 0; i < unzipStr.length; i++) {
        var chr = unzipStr.charAt(i);
        var c = _jcms_StringToAscii(chr);
        tt += chr + ":" + c + "n";
        if (parseInt("0x" + c) > 0x7f) {
            zipstr += encodeURI(unzipStr.substr(i, 1));
        } else {
            if (chr == " ")
                zipstr += "+";
            else if (strSpecial.indexOf(chr) != -1)
                zipstr += "%" + c.toString(16);
            else
                zipstr += chr;
        }
    }
    return zipstr;
}
/* Url解码 */
function _jcms_UrlDecode(zipStr) {
    var uzipStr = "";
    for (var i = 0; i < zipStr.length; i++) {
        var chr = zipStr.charAt(i);
        if (chr == "+") {
            uzipStr += " ";
        } else if (chr == "%") {
            var asc = zipStr.substring(i + 1, i + 3);
            if (parseInt("0x" + asc) > 0x7f) {
                uzipStr += decodeURI("%" + asc.toString() + zipStr.substring(i + 3, i + 9).toString());
                i += 8;
            } else {
                uzipStr += _jcms_AsciiToString(parseInt("0x" + asc));
                i += 2;
            }
        } else {
            uzipStr += chr;
        }
    }
    return uzipStr;
}
var _jcms_StringToAscii = function (str) { return str.charCodeAt(0).toString(16); }
var _jcms_AsciiToString = function (asccode) { return String.fromCharCode(asccode); }

////////////////////////////////////////////////////////////////////////////////////////////
function _jcms_SetUrlRefresh(url) {
    if (url.indexOf("?") > 0)
        return url + "&clienttime=" + Math.random();
    else
        return url + "?clienttime=" + Math.random();
}
/*刷新验证码*/
function _jcms_GetRefreshCode(obj, h) {
    if (!h)
        $i(obj).src = _jcms_SetUrlRefresh(site.Dir + "plus/getcode.aspx");
    else
        $i(obj).src = _jcms_SetUrlRefresh(site.Dir + "plus/getcode.aspx?h=" + h);
}
/*当前网站访问者信息*/
var user = new Object();
if (Lottery.Cookie.get(site.CookiePrev + "user", "id") == "") {//游客
    user.id = "0";
    user.name = "guest";
    user.userkey = "666666";
    user.groupid = "0";
    user.adminid = "0";
    user.groupname = "游客组";
    user.cookies = "88888888";
}
else {
    user.id = Lottery.Cookie.get(site.CookiePrev + "user", "id");
    user.name = Lottery.Cookie.get(site.CookiePrev + "user", "name");
    user.userkey = Lottery.Cookie.get(site.CookiePrev + "user", "userkey");
    user.groupid = Lottery.Cookie.get(site.CookiePrev + "user", "groupid");
    user.adminid = Lottery.Cookie.get(site.CookiePrev + "user", "adminid");
    user.groupname = Lottery.Cookie.get(site.CookiePrev + "user", "groupname");
    user.cookies = Lottery.Cookie.get(site.CookiePrev + "user", "cookies");
}

var _jcms_StuHover = function () {
    var cssRule;
    var newSelector;
    for (var i = 0; i < document.styleSheets.length; i++)
        for (var x = 0; x < document.styleSheets[i].rules.length; x++) {
            cssRule = document.styleSheets[i].rules[x];
            if (cssRule.selectorText.indexOf("LI:hover") != -1) {
                newSelector = cssRule.selectorText.replace(/LI:hover/gi, "LI.iehover");
                document.styleSheets[i].addRule(newSelector, cssRule.style.cssText);
            }
        }
    var topnavbar = $i("topnavbar");
    if (topnavbar != null) {
        var getElm = topnavbar.getElementsByTagName("LI");
        for (var i = 0; i < getElm.length; i++) {
            getElm[i].onmouseover = function () {
                this.className += " iehover";
            }
            getElm[i].onmouseout = function () {
                this.className = this.className.replace(new RegExp(" iehover\\b"), "");
            }
        }
    }
}
/*HTML标签小写*/
function HTML2LowerCase(html) {
    return html.replace(/(<\/?)([a-z\d\:]+)((\s+.+?)?>)/gi, function (s, a, b, c) { return a + b.toLowerCase() + c; });
}
/*获取指定字符串的长度*/
function GetLength(id) {
    var srcjo = $("#" + id);
    sType = srcjo.get(0).type;
    var len = 0;
    switch (sType) {
        case "text":
        case "hidden":
        case "password":
        case "textarea":
        case "file":
            var val = srcjo.val();
            for (var i = 0; i < val.length; i++) {
                if (val.charCodeAt(i) >= 0x4e00 && val.charCodeAt(i) <= 0x9fa5) {
                    len += 2;
                } else {
                    len++;
                }
            }
            break;
        case "checkbox":
        case "radio":
            len = $("input[type='" + sType + "'][name='" + srcjo.attr("name") + "']:checked").length;
            break;
        case "select-one":
            len = srcjo.get(0).options ? srcjo.get(0).options.selectedIndex : -1;
            break;
        case "select-more":
            break;
    }
    return len;
}
function InsertUnit(text, obj) {
    if (!obj) {
        obj = 'jstemplate';
    }
    var o = $i(obj);
    o.focus();
    if (!Lottery.isUndefined(o.selectionStart)) {
        var opn = o.selectionStart + 0;
        o.value = o.value.substr(0, o.selectionStart) + text + o.value.substr(o.selectionEnd);
    } else if (document.selection && document.selection.createRange) {
        var sel = document.selection.createRange();
        sel.text = text.replace(/\r?\n/g, '\r\n');
        //sel.moveStart('character', -strlen(text));
    } else {
        o.value += text;
    }
}
function JoinSelect(selectName) {
    var selectIDs = "";
    $("input[name='" + selectName + "']").each(function () {
        if ($(this).is(":checked")) {
            if (selectIDs == "")
                selectIDs = $(this).attr("value");
            else
                selectIDs += "," + $(this).attr("value");
        }
    })
    return selectIDs;
}

function ajaxAddMessage(userid, username) {
    window.open(site.Dir + 'user/message_send.aspx?touserid=' + userid + '&tousername=' + encodeURIComponent(username));
}
function ajaxAddFriend(userid) {
    $.ajax({
        type: "post",
        dataType: "json",
        data: "id=" + userid + "&clienttime=" + Math.random(),
        url: site.Dir + "user/ajax.aspx?oper=ajaxAddFriend",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { alert(XmlHttpRequest.responseText); } },
        success: function (d) {
            switch (d.result) {
                case '0':
                    alert(d.returnval);
                    break;
                case '1':
                    alert(d.returnval);
                    break;
            }
        }
    });
}

/*========================================================================================*/
function UrlSearch() { //重复时只取最后一个
    var name, value;
    var str = window.location.href; //取得整个地址栏
    var num = str.indexOf("?")
    str = str.substr(num + 1); //取得所有参数
    var arr = str.split("&"); //各个参数放到数组里
    for (var i = 0; i < arr.length; i++) {
        num = arr[i].indexOf("=");
        if (num > 0) {
            name = arr[i].substring(0, num);
            value = arr[i].substr(num + 1);
            this[name] = value;
        }
    }
    this["getall"] = str;
}
var RQ = new UrlSearch(); //实例化
function formatStr(s) {
    if (typeof (s) == "string")
        return s;
    else
        return "";
}
function joinValue(parameter) {
    eval("var temp=RQ." + parameter);
    if ((typeof (temp) == "string") && (typeof (temp) != null)) {
        return "&" + parameter + "=" + temp.replace(/(^\s*)|(\s*$)/g, "");
    }
    else
        return "";
}
function q(pname) {
    var query = location.search.substring(1);
    var qq = "";
    params = query.split("&");
    if (params.length > 0) {
        for (var n in params) {
            var pairs = params[n].split("=");
            if (pairs[0] == pname) {
                qq = pairs[1];
                break;
            }
        }
    }
    return qq;
}
function anchor() {
    var str = window.location.href; //取得整个地址栏
    var num = str.indexOf("#")
    str = str.substr(num + 1);
    return str;
}
/*获取当前页页码*/
function thispage() {
    var r = /^[1-9][0-9]*$/;
    if (q('page') == '') return 1;
    if (r.test(q('page')))
        return parseInt(q('page'));
    else
        return 1;
}
/*全选*/
function CheckAll(form) {
    var f;
    if (form == null)
        f = document.getElementsByTagName('FORM')[0];
    else
        f = $i(form);
    for (var i = 0; i < f.elements.length; i++) {
        var e = f.elements[i];
        if (e.name != 'chkall' && e.type == "checkbox")
            e.checked = $i("chkall").checked;
    }
}
/*全不选*/
function CheckNo(form) {
    var f;
    if (form == null)
        f = document.getElementsByTagName('FORM')[0];
    else
        f = $i(form);
    for (var i = 0; i < f.elements.length; i++) {
        var e = f.elements[i];
        if (e.type == "checkbox")
            e.checked = false;
    }
}
function WinFullOpen(url) {
    var newwin = window.open(url, "", "scrollbars");
    if (document.all) {
        newwin.moveTo(0, 0);
        newwin.resizeTo(screen.width, screen.height);
    }
}
function WindowOpen(url, iWidth, iHeight, name) {
    if (name == null) name = '';
    var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
    window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');
}
/*字符串格式化*/
String.prototype.Trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }
String.prototype.LTrim = function () { return this.replace(/(^\s*)/g, ""); }
String.prototype.RTrim = function () { return this.replace(/(\s*$)/g, ""); }

/*日期格式化(2009-06-30+++)*/
function formatDate(strDate, format) {
    return parseDate(strDate).format(format);
}
Date.prototype.format = function (format) {
    if (format == null) format = "yyyy-MM-dd HH:mm:ss";
    var o =
    {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "H+": this.getHours(),   //24小时制hour
        "h+": (this.getHours() > 12) ? (this.getHours() - 12) : this.getHours(),   //12小时制hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    }

    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}
function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);
        if (results && results.length > 3)
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10));
        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6)
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10), parseInt(results[4], 10), parseInt(results[5], 10), parseInt(results[6], 10));
        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7)
            return new Date(parseInt(results[1], 10), parseInt(results[2], 10) - 1, parseInt(results[3], 10), parseInt(results[4], 10), parseInt(results[5], 10), parseInt(results[6], 10), parseInt(results[7], 10));
    }
    return null;
}
var DateStringFromNow = function (lasttime, thistime) {
    //return thistime;
    var d_minutes, d_hours, d_days;
    var timeNow = parseInt(parseDate(thistime).getTime() / 1000);
    var d;
    d = timeNow - parseInt(parseDate(lasttime).getTime() / 1000);
    d_days = parseInt(d / 86400);
    d_hours = parseInt(d / 3600);
    d_minutes = parseInt(d / 60);
    if (d_days > 0 && d_days < 4) {
        return d_days + "天前";
    } else if (d_days <= 0 && d_hours > 0) {
        return d_hours + "小时前";
    } else if (d_hours <= 0 && d_minutes > 0) {
        return d_minutes + "分钟前";
    } else {
        return lasttime;
    }
}

/**
* 将数值四舍五入(保留2位小数)后格式化成金额形式
*
* @param num 数值(Number或者String)
* @return 金额格式的字符串,如'1,234,567.45'
* @type String
*/
function formatCurrency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
    num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}
/**
* 将数值四舍五入(保留4位小数)后格式化成金额形式
*
* @param num 数值(Number或者String)
* @return 金额格式的字符串,如'1,234,567.45'
* @type String
*/
function format4Currency(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 10000 + 0.50000000001);
    cents = num % 10000;
    num = Math.floor(num / 10000).toString();
    if (cents < 10)
        cents = "000" + cents;
    else if (cents < 100)
        cents = "00" + cents;
    else if (cents < 1000)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
    num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}

/*预览HTML代码*/
function PreviewHTML(txt) {
    var win = window.open("", "win");
    win.document.open("text/html", "replace");
    win.document.write(txt);
    win.document.close();
}

/*格式化列表*/
function FormatListValue(id) {
    var _val = $('#' + id).val();
    if (_val == '') return;
    _val = _val.replace(/[?]/g, "");
    _val = _val.replace(/[。，、.；;]/g, ",");
    _val = _val.replace(/ /g, ",");
    _val = _val.replace(/[,]+/g, ",");
    $('#' + id).val(_val);
}
function ajaxPlayCodeVoice(wmpid) {
    if (!wmpid) wmpid = "player";
    var _voicewmp2 = $i(wmpid);
    var _voicecode = "";
    $.ajax({
        type: "get",
        dataType: "json",
        data: "clienttime=" + Math.random(),
        url: _jcms_Host() + site.Dir + "ajax/other.aspx?oper=ajaxGetValidateCode",
        error: function (XmlHttpRequest, textStatus, errorThrown) { if (XmlHttpRequest.responseText != "") { alert(XmlHttpRequest.responseText); } },
        success: function (d) {
            _voicecode = d.returnval;
            _voicewmp2.innerHTML = "<embed id='sound_play' name='sound_play' src='" + site.Dir + "statics/flash/sound_play.swf' FlashVars='isPlay=1&url=" + site.Dir + "plus/codevoice.aspx&code=" + _voicecode + "' width='0' height='0' allowScriptAccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /></embed>";
        }
    });
}

// 显示当前日期，时间
function setCurrentDateTime(o) {
    var d = new Date();
    var da = d.getDate();
    var mo = d.getMonth() + 1;
    var y = d.getFullYear();
    var h = d.getHours();
    if (h < 10) { h = '0' + h }
    var m = d.getMinutes();
    if (m < 10) { m = '0' + m }
    var s = d.getSeconds();
    if (s < 10) { s = '0' + s }
    var week = ['天', '一', '二', '三', '四', '五', '六'];
    if (typeof (o) != 'object') { o = $i(o) }
    o.innerHTML = y + '年' + mo + '月' + da + '日 星期' + week[d.getDay()] + '<br />' + h + ':' + m + ':' + s;
    window.setTimeout(function () { setCurrentDateTime(o) }, 1000);
}
function CheckSearchData() {
    var type = $("#search_channeltype").val();
    if ($("#search_keywords").val() == "") {
        alert("请输入关键字");
        return;
    }
    window.open(site.Dir + 'search/default.aspx?type=' + type + '&k=' + encodeURIComponent($("#search_keywords").val()));
}
function BindModuleRadio(spanId, selecdType) {
    var data = ___JSON_Modules;
    var html = "";
    html += "<span><input id=\"RaChannelType_all\" type=\"radio\" name=\"type\" value=\"all\"";
    if (selecdType == "all")
        html += " checked=\"checked\"";
    html += " /><label for=\"RaChannelType_all\">全部</label></span>";
    for (i = 0; i < data.table.length; i++) {
        html += "<span><input id=\"RaChannelType_" + data.table[i].type + "\" type=\"radio\" name=\"type\" value=\"" + data.table[i].type + "\"";
        if (data.table[i].type == selecdType)
            html += " checked=\"checked\"";
        html += " /><label for=\"RaChannelType_" + data.table[i].type + "\">" + data.table[i].title + "</label></span>";
    }

    $("#" + spanId).html(html);
}

function stringformat(obj) {
    return escape(obj);
}

function replaceContentTags(ccid, cType, taglist, bodyid) {
    try {
        var elms1 = $("#" + bodyid + " a");
        for (i = 0; i < elms1.length; i++) { elms1[i].title = ""; }
        var elms2 = $("#" + bodyid + " img");
        for (i = 0; i < elms2.length; i++) { elms2[i].alt = ""; }
        if (taglist.length == 0) return;
        var keys = taglist.split(",");
        var element = $i(bodyid);
        for (var i = 0; i < keys.length; i++) {
            highlightWord(element, keys[i], site.Dir + 'search/default.aspx?channelid=' + ccid + '&type=' + cType + '&k=');
        }
    }
    catch (e) { }

}
function highlightWord(node, word, linkurl) {
    // Iterate into this nodes childNodes 
    if (node.hasChildNodes) {
        var hi_cn;
        for (hi_cn = 0; hi_cn < node.childNodes.length; hi_cn++) {
            highlightWord(node.childNodes[hi_cn], word, linkurl);
        }
    }
    // And do this node itself 
    if (node.nodeType == 3) { // text node 
        tempNodeVal = node.nodeValue.toLowerCase();
        tempWordVal = word.toLowerCase();
        if (tempNodeVal.indexOf(tempWordVal) > -1) {
            pn = node.parentNode;
            if (pn.className != "highlight") {
                nv = node.nodeValue;
                ni = tempNodeVal.indexOf(tempWordVal);
                before = document.createTextNode(nv.substr(0, ni));
                docWordVal = nv.substr(ni, word.length);
                after = document.createTextNode(nv.substr(ni + word.length));
                hiwordtext = document.createTextNode(docWordVal);
                hiword = document.createElement("A");
                hiword.className = "highlight";
                if (linkurl)
                    hiword.href = linkurl + encodeURIComponent(tempWordVal);
                hiword.appendChild(hiwordtext);
                pn.insertBefore(before, node);
                pn.insertBefore(hiword, node);
                pn.insertBefore(after, node);
                pn.removeChild(node);
            }
        }
    }

}
/*选项卡*/
function $SetTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {
        var menu = $('#menu_' + name + i);
        var cont = $('#cont_' + name + i);
        if (i == cursel) {
            menu.addClass('current');
            cont.show();
        } else {
            menu.removeClass('current');
            cont.hide();
        }
    }
}

function jTab(Id, tId, EclassName, iBeHavior) {
    if (!document.getElementById(Id)) return;
    if (iBeHavior == null) iBeHavior = 'mouseover';
    if (EclassName == null) EclassName = 'more';
    var self = this;
    var links = document.getElementById(Id).getElementsByTagName('a');
    if (links.length == 0) return;

    this.init = function () {
        for (var i = 0; i < links.length; i++) {
            eval("links[i].on" + iBeHavior + "=function(e){return self.itab(this);};");
            links[i].onclick = function () {
                return (this.href.indexOf('javascript:') > -1 || this.href.indexOf('#') < 0 || this.className == EclassName);
            };
            links[i].onfocus = function (e) {
                this.blur();
            };
        }
        self.itab(links[0]);
    };
    this.itab = function (o) {
        if (o.href.indexOf('javascript:') > -1 || o.href.indexOf('#') < 0 || o.className == EclassName) { return true; }
        for (var i = 0; i < links.length; i++) {
            if (links[i].className != EclassName) links[i].className = '';
        }
        o.className = 's';
        var url = o.href.substring(o.href.indexOf('#') + 1);
        this.showDiv(url);
        return false;
    };
    this.showDiv = function (tDiv) {
        if (document.getElementById(tId) && document.getElementById(tDiv)) {
            document.getElementById(tId).innerHTML = document.getElementById(tDiv).innerHTML;
            jTab_img_border(document.getElementById(tId));
            //jTab_blank_link(document.getElementById(tId));
            jTab_set_className(document.getElementById(tId));
        }
    };
    this.createDiv = function (id) {
        var div = document.createElement('div');
        div.style.display = 'none';
        div.id = id;
        document.body.appendChild(div);
        return div;
    };
    this.init();
}

function jTab_img_border(obj) {
    var li = obj.getElementsByTagName('li');
    var img = null;
    var bc = '#333';
    for (var i = 0; i < li.length; i++) {
        img = li[i].getElementsByTagName('img');
        for (var j = 0; j < img.length; j++) {
            bc = img[j].style.borderColor;
            img[j].onmouseover = function () { this.style.borderColor = '#f60'; };
            img[j].onmouseout = function () { this.style.borderColor = bc; };
        }
    }
}

function jTab_blank_link(obj) {
    obj = obj == null ? document : obj;
    var links = obj.getElementsByTagName('a');
    for (var j = 0; j < links.length; j++) {
        links[j].setAttribute('target', '_blank');
    }
}

function jTab_set_className(obj) {
    obj = obj == null ? document : obj;
    this.initialize = function () {
        var ename = '';
        var links = obj.getElementsByTagName('a');
        for (var i = 0; i < links.length; i++) {
            ename = links[i].className;
            if (ename == 'new' || ename == 'hot') {
                links[i].style.position = 'relative';
                this.createDiv(links[i], ename);
            }
        }
    };
    this.createDiv = function (ilink, en) {
        var a = document.createElement('div');
        a.className = 'icon_' + en;
        a.style.left = parseInt(ilink.offsetLeft - 15) + 'px';
        a.style.top = parseInt(ilink.offsetTop - 15) + 'px';
        ilink.parentNode.appendChild(a);
        return a;
    };
    this.initialize();
}

/*图片滚动*/
var $core = { isIE: navigator.appVersion.indexOf("MSIE") != -1 ? true : false, addEvent: function (l, i, I) { if (l.attachEvent) { l.attachEvent("on" + i, I) } else { l.addEventListener(i, I, false) } }, delEvent: function (l, i, I) { if (l.detachEvent) { l.detachEvent("on" + i, I) } else { l.removeEventListener(i, I, false) } }, readCookie: function (O) { var o = "", l = O + "="; if (document.cookie.length > 0) { var i = document.cookie.indexOf(l); if (i != -1) { i += l.length; var I = document.cookie.indexOf(";", i); if (I == -1) I = document.cookie.length; o = unescape(document.cookie.substring(i, I)) } }; return o }, writeCookie: function (i, l, o, c) { var O = "", I = ""; if (o != null) { O = new Date((new Date).getTime() + o * 3600000); O = "; expires=" + O.toGMTString() }; if (c != null) { I = ";domain=" + c }; document.cookie = i + "=" + escape(l) + O + I }, readStyle: function (I, l) { if (I.style[l]) { return I.style[l] } else if (I.currentStyle) { return I.currentStyle[l] } else if (document.defaultView && document.defaultView.getComputedStyle) { var i = document.defaultView.getComputedStyle(I, null); return i.getPropertyValue(l) } else { return null } } };

function ScrollPic(scrollContId, arrLeftId, arrRightId, dotListId) { this.scrollContId = scrollContId; this.arrLeftId = arrLeftId; this.arrRightId = arrRightId; this.dotListId = dotListId; this.dotClassName = "dotItem"; this.dotOnClassName = "dotItemOn"; this.dotObjArr = []; this.pageWidth = 0; this.frameWidth = 0; this.speed = 10; this.space = 10; this.pageIndex = 0; this.autoPlay = true; this.autoPlayTime = 5; var _autoTimeObj, _scrollTimeObj, _state = "ready"; this.stripDiv = document.createElement("DIV"); this.listDiv01 = document.createElement("DIV"); this.listDiv02 = document.createElement("DIV"); if (!ScrollPic.childs) { ScrollPic.childs = [] }; this.ID = ScrollPic.childs.length; ScrollPic.childs.push(this); this.initialize = function () { if (!this.scrollContId) { throw new Error("必须指定scrollContId."); return }; this.scrollContDiv = $i(this.scrollContId); if (!this.scrollContDiv) { throw new Error("scrollContId不是正确的对象.(scrollContId = \"" + this.scrollContId + "\")"); return }; this.scrollContDiv.style.width = this.frameWidth + "px"; this.scrollContDiv.style.overflow = "hidden"; this.listDiv01.innerHTML = this.listDiv02.innerHTML = this.scrollContDiv.innerHTML; this.scrollContDiv.innerHTML = ""; this.scrollContDiv.appendChild(this.stripDiv); this.stripDiv.appendChild(this.listDiv01); this.stripDiv.appendChild(this.listDiv02); this.stripDiv.style.overflow = "hidden"; this.stripDiv.style.zoom = "1"; this.stripDiv.style.width = "32766px"; this.listDiv01.style.cssFloat = "left"; this.listDiv02.style.cssFloat = "left"; $core.addEvent(this.scrollContDiv, "mouseover", Function("ScrollPic.childs[" + this.ID + "].stop()")); $core.addEvent(this.scrollContDiv, "mouseout", Function("ScrollPic.childs[" + this.ID + "].play()")); if (this.arrLeftId) { this.arrLeftObj = $i(this.arrLeftId); if (this.arrLeftObj) { $core.addEvent(this.arrLeftObj, "mousedown", Function("ScrollPic.childs[" + this.ID + "].rightMouseDown()")); $core.addEvent(this.arrLeftObj, "mouseup", Function("ScrollPic.childs[" + this.ID + "].rightEnd()")); $core.addEvent(this.arrLeftObj, "mouseout", Function("ScrollPic.childs[" + this.ID + "].rightEnd()")) } }; if (this.arrRightId) { this.arrRightObj = $i(this.arrRightId); if (this.arrRightObj) { $core.addEvent(this.arrRightObj, "mousedown", Function("ScrollPic.childs[" + this.ID + "].leftMouseDown()")); $core.addEvent(this.arrRightObj, "mouseup", Function("ScrollPic.childs[" + this.ID + "].leftEnd()")); $core.addEvent(this.arrRightObj, "mouseout", Function("ScrollPic.childs[" + this.ID + "].leftEnd()")) } }; if (this.dotListId) { this.dotListObj = $i(this.dotListId); if (this.dotListObj) { var pages = Math.round(this.listDiv01.offsetWidth / this.frameWidth + 0.4), i, tempObj; for (i = 0; i < pages; i++) { tempObj = document.createElement("span"); this.dotListObj.appendChild(tempObj); this.dotObjArr.push(tempObj); if (i == this.pageIndex) { tempObj.className = this.dotClassName } else { tempObj.className = this.dotOnClassName }; tempObj.title = "第" + (i + 1) + "页"; $core.addEvent(tempObj, "click", Function("ScrollPic.childs[" + this.ID + "].pageTo(" + i + ")")) } } }; if (this.autoPlay) { this.play() } }; this.leftMouseDown = function () { if (_state != "ready") { return }; _state = "floating"; _scrollTimeObj = setInterval("ScrollPic.childs[" + this.ID + "].moveLeft()", this.speed) }; this.rightMouseDown = function () { if (_state != "ready") { return }; _state = "floating"; _scrollTimeObj = setInterval("ScrollPic.childs[" + this.ID + "].moveRight()", this.speed) }; this.moveLeft = function () { if (this.scrollContDiv.scrollLeft + this.space >= this.listDiv01.scrollWidth) { this.scrollContDiv.scrollLeft = this.scrollContDiv.scrollLeft + this.space - this.listDiv01.scrollWidth } else { this.scrollContDiv.scrollLeft += this.space }; this.accountPageIndex() }; this.moveRight = function () { if (this.scrollContDiv.scrollLeft - this.space <= 0) { this.scrollContDiv.scrollLeft = this.listDiv01.scrollWidth + this.scrollContDiv.scrollLeft - this.space } else { this.scrollContDiv.scrollLeft -= this.space }; this.accountPageIndex() }; this.leftEnd = function () { if (_state != "floating") { return }; _state = "stoping"; clearInterval(_scrollTimeObj); var fill = this.pageWidth - this.scrollContDiv.scrollLeft % this.pageWidth; this.move(fill) }; this.rightEnd = function () { if (_state != "floating") { return }; _state = "stoping"; clearInterval(_scrollTimeObj); var fill = -this.scrollContDiv.scrollLeft % this.pageWidth; this.move(fill) }; this.move = function (num, quick) { var thisMove = num / 5; if (!quick) { if (thisMove > this.space) { thisMove = this.space }; if (thisMove < -this.space) { thisMove = -this.space } }; if (Math.abs(thisMove) < 1 && thisMove != 0) { thisMove = thisMove >= 0 ? 1 : -1 } else { thisMove = Math.round(thisMove) }; var temp = this.scrollContDiv.scrollLeft + thisMove; if (thisMove > 0) { if (this.scrollContDiv.scrollLeft + thisMove >= this.listDiv01.scrollWidth) { this.scrollContDiv.scrollLeft = this.scrollContDiv.scrollLeft + thisMove - this.listDiv01.scrollWidth } else { this.scrollContDiv.scrollLeft += thisMove } } else { if (this.scrollContDiv.scrollLeft - thisMove <= 0) { this.scrollContDiv.scrollLeft = this.listDiv01.scrollWidth + this.scrollContDiv.scrollLeft - thisMove } else { this.scrollContDiv.scrollLeft += thisMove } }; num -= thisMove; if (Math.abs(num) == 0) { _state = "ready"; if (this.autoPlay) { this.play() }; this.accountPageIndex(); return } else { this.accountPageIndex(); setTimeout("ScrollPic.childs[" + this.ID + "].move(" + num + "," + quick + ")", this.speed) } }; this.next = function () { if (_state != "ready") { return }; _state = "stoping"; this.move(this.pageWidth, true) }; this.play = function () { if (!this.autoPlay) { return }; clearInterval(_autoTimeObj); _autoTimeObj = setInterval("ScrollPic.childs[" + this.ID + "].next()", this.autoPlayTime * 1000) }; this.stop = function () { clearInterval(_autoTimeObj) }; this.pageTo = function (num) { if (_state != "ready") { return }; _state = "stoping"; var fill = num * this.frameWidth - this.scrollContDiv.scrollLeft; this.move(fill, true) }; this.accountPageIndex = function () { this.pageIndex = Math.round(this.scrollContDiv.scrollLeft / this.frameWidth); if (this.pageIndex > Math.round(this.listDiv01.offsetWidth / this.frameWidth + 0.4) - 1) { this.pageIndex = 0 }; var i; for (i = 0; i < this.dotObjArr.length; i++) { if (i == this.pageIndex) { this.dotObjArr[i].className = this.dotClassName } else { this.dotObjArr[i].className = this.dotOnClassName } } } };

/*内容滚动*/
function jScrollText(content, btnPrevious, btnNext, autoStart, timeout, isSmoothScroll) {
    this.Speed = 10;
    this.Timeout = timeout;
    this.stopscroll = false; //是否停止滚动的标志位
    this.isSmoothScroll = isSmoothScroll; //是否平滑连续滚动
    this.LineHeight = 20; //默认高度。可以在外部根据需要设置
    this.NextButton = this.$(btnNext);
    this.PreviousButton = this.$(btnPrevious);
    this.ScrollContent = this.$(content);
    if (!this.ScrollContent) return;
    this.ScrollContent.innerHTML += this.ScrollContent.innerHTML; //为了平滑滚动再加一遍

    if (this.PreviousButton) {
        this.PreviousButton.onclick = this.GetFunction(this, "Previous");
        this.PreviousButton.onmouseover = this.GetFunction(this, "MouseOver");
        this.PreviousButton.onmouseout = this.GetFunction(this, "MouseOut");
    }
    if (this.NextButton) {
        this.NextButton.onclick = this.GetFunction(this, "Next");
        this.NextButton.onmouseover = this.GetFunction(this, "MouseOver");
        this.NextButton.onmouseout = this.GetFunction(this, "MouseOut");
    }
    this.ScrollContent.onmouseover = this.GetFunction(this, "MouseOver");
    this.ScrollContent.onmouseout = this.GetFunction(this, "MouseOut");

    if (autoStart) {
        this.Start();
    }
}

jScrollText.prototype = {

    $: function (element) {
        return document.getElementById(element);
    },
    Previous: function () {
        this.stopscroll = true;
        this.Scroll("up");
    },
    Next: function () {
        this.stopscroll = true;
        this.Scroll("down");
    },
    Start: function () {
        if (this.isSmoothScroll) {
            this.AutoScrollTimer = setInterval(this.GetFunction(this, "SmoothScroll"), this.Timeout);
        }
        else {
            this.AutoScrollTimer = setInterval(this.GetFunction(this, "AutoScroll"), this.Timeout);
        }
    },
    Stop: function () {
        clearTimeout(this.AutoScrollTimer);
        this.DelayTimerStop = 0;
    },
    MouseOver: function () {
        this.stopscroll = true;
    },
    MouseOut: function () {
        this.stopscroll = false;
    },
    AutoScroll: function () {
        if (this.stopscroll) {
            return;
        }
        this.ScrollContent.scrollTop++;
        if (parseInt(this.ScrollContent.scrollTop) % this.LineHeight != 0) {
            this.ScrollTimer = setTimeout(this.GetFunction(this, "AutoScroll"), this.Speed);
        }
        else {
            if (parseInt(this.ScrollContent.scrollTop) >= parseInt(this.ScrollContent.scrollHeight) / 2) {
                this.ScrollContent.scrollTop = 0;
            }
            clearTimeout(this.ScrollTimer);
            //this.AutoScrollTimer = setTimeout(this.GetFunction(this,"AutoScroll"), this.Timeout);
        }
    },
    SmoothScroll: function () {
        if (this.stopscroll) {
            return;
        }
        this.ScrollContent.scrollTop++;
        if (parseInt(this.ScrollContent.scrollTop) >= parseInt(this.ScrollContent.scrollHeight) / 2) {
            this.ScrollContent.scrollTop = 0;
        }
    },
    Scroll: function (direction) {

        if (direction == "up") {
            this.ScrollContent.scrollTop--;
        }
        else {
            this.ScrollContent.scrollTop++;
        }
        if (parseInt(this.ScrollContent.scrollTop) >= parseInt(this.ScrollContent.scrollHeight) / 2) {
            this.ScrollContent.scrollTop = 0;
        }
        else if (parseInt(this.ScrollContent.scrollTop) <= 0) {
            this.ScrollContent.scrollTop = parseInt(this.ScrollContent.scrollHeight) / 2;
        }

        if (parseInt(this.ScrollContent.scrollTop) % this.LineHeight != 0) {
            this.ScrollTimer = setTimeout(this.GetFunction(this, "Scroll", direction), this.Speed);
        }
    },
    GetFunction: function (variable, method, param) {
        return function () {
            variable[method](param);
        }
    }
}

var isIE = ! -[1, ];
//JS图片播放器
function renderPicPlayer(id) {
    var interv = 4000; //切换间隔时间
    var intervSpeed = 10; //切换速度
    var cpic = 0;
    var tpic = 1;
    var timer, timer1, timer2;

    var list = $i(id + '-list');
    if (list) { list = list.getElementsByTagName('li') }
    var change = $i(id + '-change');
    if (!list || !list.length || list.length < 2 || !change) { return }

    var lis = cls = '';
    var picnum = list.length;
    for (var i = 0; i < picnum; i++) { cls = i == 0 ? ' class="active"' : ''; lis += '<li' + cls + '>' + (i + 1) + '</li>' }
    change.innerHTML = lis;
    change = change.getElementsByTagName('li');
    var div = list[0].getElementsByTagName('div')[0];
    var img_fit_with = div.offsetWidth, img_fit_height = div.offsetHeight;
    for (var i = 0; i < picnum; i++) {
        change[i].index = i;
        var img = list[i].getElementsByTagName('img');
        if (img && img[0]) {
            //img[0].onload = function(){resizeImage(this, img_fit_with, img_fit_height, true)}
        }
        if (i > 0) {
            list[i].opacity = 0;
            alpha(list[i]);
        } else {
            list[i].opacity = 100;
        }
        change[i].onmouseover = function () {
            list[cpic].opacity = 0;
            alpha(list[cpic]);
            setActive(cpic);
            cpic = tpic = this.index;
            list[tpic].opacity = 100;
            alpha(list[tpic]);
            setActive(tpic, true);
            tpic = tpic == (picnum - 1) ? 0 : tpic + 1;
            window.clearInterval(timer);
            timer = window.setInterval(loop, interv);
        }
    }
    function setActive(n, f) { change[n].className = f ? 'active' : '' }
    if (picnum < 2) { return }
    //控制图层透明度
    function alpha(o) { if (isIE) { o.style.filter = "alpha(opacity=" + o.opacity + ")"; } else { o.style.opacity = (o.opacity / 100) } o.style.display = o.opacity > 0 ? '' : 'none' }
    //渐显
    var fadeon = function () { setActive(tpic, true); var o = list[tpic]; o.opacity += 5; alpha(o); if (o.opacity < 100) { window.clearTimeout(timer1); timer1 = setTimeout(fadeon, intervSpeed) } else { cpic = tpic; tpic = tpic == (picnum - 1) ? 0 : tpic + 1; } }
    //渐隐
    var fadeout = function () { setActive(cpic); var o = list[cpic]; o.opacity -= 10; alpha(o); if (o.opacity > 0) { window.clearTimeout(timer2); timer2 = setTimeout(fadeout, intervSpeed) } else { o.opacity = 0; } }
    //循环
    var loop = function () { fadeout(); setTimeout(fadeon, intervSpeed + 50) }
    timer = window.setInterval(loop, interv);
}
/*加入收藏夹*/
function _jcms_addFavorite(A, C) {
    if (window.sidebar || window.opera) {
        return true;
    }
    try { window.external.AddFavorite(A, C); }
    catch (B) {
        alert("你可以按Ctrl+D键收藏本页！");
    }
    return false;
}
function _jcms_GetPageContent(totalCount, pageSize, currentPage) {
    if (totalCount == 0) return;
    var _html = "";
    if (totalCount % pageSize == 0)
        pageFoot = Math.floor(totalCount / pageSize);
    else
        pageFoot = Math.floor(totalCount / pageSize) + 1;
    var liRoot = (currentPage - 1) * pageSize + 1;
    var liFoot = currentPage * pageSize;
    if (liFoot > totalCount) liFoot = totalCount;
    _html += "<div class='p_btns'>";
    for (var i = 1; i <= pageFoot; i++) {
        if (i == currentPage)
            _html += "<span class='currentpage'>" + i + "</span>";
        else
            _html += "<a target='_self' href='javascript:;' onclick='_jcms_GetPageContent(" + totalCount + "," + pageSize + "," + i + ")'>" + i + "</a>";
    }
    _html += "</div>";
    $("#PageBar").html(_html);
    for (var j = 1; j <= totalCount; j++) {
        if (j >= liRoot && j <= liFoot)
            $("#Repeat_" + j).show();
        else
            $("#Repeat_" + j).hide();
    }
}
function _jcms_SearchBar(type) {
    if (!type || type == '') type = 'all';
    var _document = "<div class=\"search_bar\">";
    _document += "    <div id=\"search_bar_date\" class=\"search_bar_left\"></div>";
    _document += "    <script type=\"text/javascript\">setCurrentDateTime('search_bar_date');<\/script>";
    _document += "    <form id=\"searchform\" target=\"_blank\" action=\"" + site.Dir + "search/default.aspx\"><input type=\"hidden\" name=\"channelid\" value=\"0\" />";
    _document += "<ul class=\"tab\" id=\"sotypetab\">";
    _document += "<li class=\"new\"><span id=\"ajaxChannelType\"></span><script>BindModuleRadio('ajaxChannelType','" + type + "');</script></li>";
    _document += "</ul>";
    _document += "        <div class=\"sokey\">";
    _document += "            <input type=\"text\" name=\"k\" id=\"search_keywords\" class=\"keywords\" value=\"\" />";
    _document += "            <input type=\"submit\" id=\"searchsubmit\" class=\"submit\" value=\"搜索\" />";
    _document += "        </div>";
    _document += "    </form>";
    _document += "    <div id=\"search_bar_weather\" class=\"search_bar_right\"></div><script>Lottery.Core.ShowWeather(\"search_bar_weather\");</script>";
    _document += "</div>";
    return _document;
}