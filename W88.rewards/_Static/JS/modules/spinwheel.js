﻿document.onkeypress = function(event) {
    event = (event || window.event);
    if (event.keyCode == 123 || event.keyCode == 73) {
        return false;
    }
};

document.onmousedown = function(event) {
    event = (event || window.event);
    if (event.keyCode == 123 || event.keyCode == 73) {
        return false;
    }
};

document.onkeydown = function(event) {
    event = (event || window.event);
    if (event.keyCode == 123 || event.keyCode == 73) {
        return false;
    }
};

if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    };
}

function clickIE() { if (document.all) { return false; } }
function clickNS(e) {
    if(document.layers || (document.getElementById && !document.all)) {
        if (e.which == 2 || e.which == 3) { return false; }
    }
};

if (document.layers)
{ document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
document.oncontextmenu = new Function("return false");

var _sid = 0;
	
var SW = function(user, translations, swc, pic, isMobile, isLoggedIn, lang) {
    this.u = user;
    this.t = translations;
    this.ca = [];
    this.im = isMobile;
    this.swp = [];
    this.hl = false;
    this.is = false;
    this.swr = {};
    this.wp = {};
    this.la = 0;
    this.swc = swc;
    this.pic = pic;
    this.siid = 0;
    this.ciid = 0;
    this.isLoggedIn = isLoggedIn;
    this.sk = window.location.host + '_prizes_' + lang;
};

window.addEventListener("orientationchange", function () {
    if (!sw) return;
    sw._rdc_();
});

window.onload = function () {
    if (!sw.swc) 
        sw._gcd_();

    sw._gwc_();
    sw._db_();
    sw._isw_();
    document.getElementById('spinWheel').addEventListener('wheel', function () { return false; });
    sw._obsM_();
};

SW.prototype._gwc_ = function () {
    var self = this,
        ca = [];
    switch (self.swc.w) {
        case 604:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 125, b: -301 };
            ca[2] = { a: 0, b: -605 };
            ca[3] = { a: -301, b: -730 };
            ca[4] = { a: -606, b: -605 };
            ca[5] = { a: -730, b: -300 };
            ca[6] = { a: -605, b: 0 };
            ca[7] = { a: -305, b: 125 };
            if(self.im)
                self.pic = { w: 408, h: 328, a: -147, b: -10 };             
            break;
        case 507:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 105, b: -251 };
            ca[2] = { a: 0, b: -509 };
            ca[3] = { a: -253, b: -613 };
            ca[4] = { a: -505, b: -505 };
            ca[5] = { a: -610, b: -250 };
            ca[6] = { a: -510, b: 0 };
            ca[7] = { a: -250, b: 102 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -197, b: -27 };
            break;
        case 529:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 112, b: -266 };
            ca[2] = { a: 0, b: -530 };
            ca[3] = { a: -263, b: -640 };
            ca[4] = { a: -530, b: -532 };
            ca[5] = { a: -639, b: -270 };
            ca[6] = { a: -530, b: -4 };
            ca[7] = { a: -266, b: 105 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -197, b: -27 };
            break;
        case 534:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 112, b: -266 };
            ca[2] = { a: 0, b: -534 };
            ca[3] = { a: -268, b: -645 };
            ca[4] = { a: -535, b: -532 };
            ca[5] = { a: -644, b: -270 };
            ca[6] = { a: -535, b: -4 };
            ca[7] = { a: -266, b: 105 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -197, b: -27 };
            break;
        case 536:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 112, b: -266 };
            ca[2] = { a: 0, b: -537 };
            ca[3] = { a: -268, b: -650 };
            ca[4] = { a: -535, b: -532 };
            ca[5] = { a: -644, b: -270 };
            ca[6] = { a: -538, b: 0 };
            ca[7] = { a: -266, b: 105 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -197, b: -27 };
            break;
        case 464:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 98, b: -231 };
            ca[2] = { a: 0, b: -466 };
            ca[3] = { a: -230, b: -562 };
            ca[4] = { a: -464, b: -465 };
            ca[5] = { a: -560, b: -230 };
            ca[6] = { a: -465, b: 0 };
            ca[7] = { a: -230, b: 95 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -197, b: -27 };
            break;
        case 398:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 82, b: -200 };
            ca[2] = { a: 0, b: -400 };
            ca[3] = { a: -197, b: -480 };
            ca[4] = { a: -397, b: -395 };
            ca[5] = { a: -480, b: -198 };
            ca[6] = { a: -400, b: 0 };
            ca[7] = { a: -197, b: 79 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -95, b: -12 };
            break;
        case 368:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 77, b: -180 };
            ca[2] = { a: 2, b: -370 };
            ca[3] = { a: -183, b: -440 };
            ca[4] = { a: -368, b: -360 };
            ca[5] = { a: -440, b: -183 };
            ca[6] = { a: -368, b: 0 };
            ca[7] = { a: -182, b: 75 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -112, b: -22 };
            break;
        case 359:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 75, b: -175 };
            ca[2] = { a: 0, b: -367 };
            ca[3] = { a: -180, b: -430 };
            ca[4] = { a: -357, b: -360 };
            ca[5] = { a: -434, b: -180 };
            ca[6] = { a: -360, b: 0 };
            ca[7] = { a: -178, b: 70 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -112, b: -22 };
            break;
        case 344:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 73, b: -171 };
            ca[2] = { a: 0, b: -345 };
            ca[3] = { a: -171, b: -410 };
            ca[4] = { a: -342, b: -345 };
            ca[5] = { a: -415, b: -172 };
            ca[6] = { a: -345, b: 0 };
            ca[7] = { a: -170, b: 69 };
            if(self.im)
                self.pic = { w: 408, h: 328, a: -121, b: -20 };
            break;
        case 304:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 65, b: -151 };
            ca[2] = { a: 0, b: -305 };
            ca[3] = { a: -151, b: -365 };
            ca[4] = { a: -303, b: -305 };
            ca[5] = { a: -367, b: -150 };
            ca[6] = { a: -305, b: 0 };
            ca[7] = { a: -152, b: 63 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -141, b: -25 };
            break;
        default:
            ca[0] = { a: 0, b: 0 };
            ca[1] = { a: 125, b: -301 };
            ca[2] = { a: 0, b: -605 };
            ca[3] = { a: -301, b: -730 };
            ca[4] = { a: -606, b: -605 };
            ca[5] = { a: -730, b: -300 };
            ca[6] = { a: -605, b: 0 };
            ca[7] = { a: -305, b: 125 };
            if (self.im)
                self.pic = { w: 408, h: 328, a: -147, b: -10 };
            break;
    }
    self.ca = ca;
};

SW.prototype._obsM_ = function() {
    try {
        var _mo = (function() {
            var prefixes = ['WebKit', 'Moz', 'O', 'Ms', ''];
            for (var i = 0; i < prefixes.length; i++) {
                if (prefixes[i] + 'MutationObserver' in window) {
                    return window[prefixes[i] + 'MutationObserver'];
                }
            }
            return false;
        }());

        if (!_mo) {
            return;
        }

        var self = this,
            _t1 = document.getElementById('spinWheelContainer'),
            _obs1 = new MutationObserver(function(mutations) {
                mutations.forEach(function(mutation) {
                    if (!mutation || !mutation.target || !mutation.target.attributes || !mutation.target.attributes.id) return;
                    var id = mutation.target.attributes.id.nodeValue;
                    if (self.hl && !self.is && id === 'spinWheel') {
                        var _tr = mutation.target.style.transform || mutation.target.style.WebkitTransform || mutation.target.style.MozTransform || mutation.target.style.msTransform;
                        if (_.isEmpty(_tr)) return;
                        if (self.la !== parseInt((_tr.split('rotate(')[1]).split('deg)')[0])) {
                            document.getElementById('spinWheel').style.visibility = 'hidden';
                            window.location.reload();
                        }
                    }
                });
            }),
            _t2 = document.getElementById('prizeModal'),
            _obs2 = new MutationObserver(function(mutations) {
                mutations.forEach(function(mutation) {
                    if (!mutation || !mutation.target || !mutation.target.attributes || !mutation.target.attributes.id) return;
                    var id = mutation.target.attributes.id.nodeValue;
                    if (self.hl && !self.is && id === 'prizeContainer') {
                        document.getElementById('spinWheel').style.visibility = 'hidden';
                        window.location.reload();
                    }
                });
            }),
            _cfg = {
                attributes: true,
                childList: true,
                characterData: true,
                subtree: true,
                attributeFilter: ['style']
            };

        // pass in the target node, as well as the observer options
        _obs1.observe(_t1, _cfg);
        _obs2.observe(_t2, _cfg);
    } catch (e) {}
};

function _dg_(obj, prop, fn) {

    if (Object.defineProperty)
        return Object.defineProperty(obj, prop, fn);
    if (Object.prototype.__defineGetter__)
        return obj.__defineGetter__(prop, fn.get);

    throw new Error("no support");
}

function _ds_(obj, prop, fn) {

    if (Object.defineProperty)
        return Object.defineProperty(obj, prop, fn);
    if (Object.prototype.__defineSetter__)
        return obj.__defineSetter__(prop, fn.set);

    throw new Error("no support");
}

SW.prototype._dsw_ = function() {
    var c = document.getElementById('swc'),
        ctx = c.getContext('2d'),
        imgs = {},
        ri = new Image(),
        self = this;
    ctx.canvas.height = self.swc.h;
    ctx.canvas.width = self.swc.w;
    ctx.save();
    for (var x = 0; x < 8; x++) {
        imgs[x] = new Image();
    }

    ri.src = '/_static/Images/Spinwheel/spinwheel2.png';
    ri.onload = function () {
        ctx.drawImage(ri, 0, 0, self.swc.w, self.swc.h);
        if (_.isEmpty(self.swp)) return;
        imgs[0].src = self.swp[0].is;
        imgs[0].onload = function() {
            // Draw prize 1
            ctx.drawImage(imgs[0], self.ca[0].a, self.ca[0].b, self.swc.w, self.swc.h);
            imgs[1].src = self.swp[1].is;
            imgs[1].onload = function() {
                // Draw prize 2
                self._rpi_(ctx, imgs[1], 45, self.ca[1].a, self.ca[1].b);
                imgs[2].src = self.swp[2].is;
                imgs[2].onload = function() {
                    // Draw prize 3
                    self._rpi_(ctx, imgs[2], 90, self.ca[2].a, self.ca[2].b);
                    imgs[3].src = self.swp[3].is;
                    imgs[3].onload = function() {
                        // Draw prize 4
                        self._rpi_(ctx, imgs[3], 135, self.ca[3].a, self.ca[3].b);
                        imgs[4].src = self.swp[4].is;
                        imgs[4].onload = function() {
                            // Draw prize 5
                            self._rpi_(ctx, imgs[4], 180, self.ca[4].a, self.ca[4].b);
                            imgs[5].src = self.swp[5].is;
                            imgs[5].onload = function() {
                                // Draw image 6
                                self._rpi_(ctx, imgs[5], 225, self.ca[5].a, self.ca[5].b);
                                imgs[6].src = self.swp[6].is;
                                imgs[6].onload = function() {
                                    // Draw image 7
                                    self._rpi_(ctx, imgs[6], 270, self.ca[6].a, self.ca[6].b);
                                    imgs[7].src = self.swp[7].is;
                                    imgs[7].onload = function() {
                                        // Draw image 8
                                        self._rpi_(ctx, imgs[7], 315, self.ca[7].a, self.ca[7].b);
                                    };
                                };
                            };
                        };
                    };
                };
            };
        };
    };
};

SW.prototype._rpi_ = function (ctx, img, deg, x, y) {
    var self = this;
    ctx.rotate(deg * Math.PI / 180);
    ctx.drawImage(img, x, y, self.swc.w, self.swc.h);
    ctx.restore();
    ctx.save();
};

SW.prototype._isw_ = function() {
    var self = this;
    if (!self.isLoggedIn) {
        $('#loginFrame').modal('show');
        return;
    }
    self._t_(0);
    self.swr = null;
    self.u['CachedPrizeItems'] = null;
    $.ajax({
        type: 'GET',
        async: true,
        url: '/api/rewards/spinwheel/initialize',
        contentType: 'application/json',
        data: self.u,
        success: function(response) {
            try {
                if (!response) {
                    self._ssw_(true);
                    return;
                }
                self.swr = response;
                self._iv_();
            } catch (e) {
                self.swr = null;
                self._ssw_(true);
                return;
            }
            $('#spinWheelContent').show();
        },
        error: function() {
            self._ssw_(true);          
        }
    });
};

SW.prototype._iv_ = function (isSpin) {
    var self = this;
    if (!self.swr) {
        self._sem_(null, !isSpin);
        return;
    }
    var data = self.swr.ResponseData;
    if (_.isEmpty(data) || _.isEmpty(data.PrizeItems)) {
        self._sem_(null, !isSpin);
        return;
    }
    self.swp = [];
    _.each(data.PrizeItems, function(p, i) {
        var index = i + 1,
            r = 0,
            o = 0;
        switch (index) {
            case 1:
                r = 0;
                o = 360;
                break;
            case 2:
                r = 45;
                o = 315;
                break;
            case 3:
                r = 90;
                o = 270;
                break;
            case 4:
                r = 135;
                o = 225;
                break;
            case 5:
                r = 180;
                o = 180;
                break;
            case 6:
                r = 225;
                o = 135;
                break;
            case 7:
                r = 270;
                o = 90;
                break;
            case 8:
                r = 315;
                o = 45;
                break;
        }

        if (p) {
            document.getElementById('prize' + index).innerHTML = p.PrizeName;
            self.swp.push({
                n: 'prize' + index,
                r: r,
                o: o,
                i: i,
                is: p.ImagePath,
                pn: p.PrizeName,
                c: p.ProductCode
            });
        } 
    });
    var prizes = amplify.store(self.sk);
    if (!_.isEmpty(prizes)) {
        var pdiff = _.filter(prizes, function (prize, index) {
                if(!_.isEqual(prize, self.swp[index])) 
                    return prize;
            });
        if (!_.isEmpty(pdiff)) {
            self._rdc_(true);
            self._ssw_(!isSpin);
            return;
        }
    } 
    amplify.store(self.sk, self.swp);
    self._dsw_();
    self._ssw_(!isSpin);
};

SW.prototype._sr_ = function() {
    var self = this,
        degrees = 0;
    self.siid = setInterval(function() {
        if (degrees < 360) {
            degrees += 15;
        } else {
            degrees = 0;
        }
        self._t_(degrees);
    }, 50);
};

SW.prototype._is_ = function () {
    var self = this;
    self._ar_(self.la, self.la - 20, 600, false, true);
};

SW.prototype._ar_ = function(fromDegrees, toDegrees, duration, hasError, isInit) {
    var self = this;
    $({ deg: fromDegrees }).animate({ deg: toDegrees }, {
        duration: duration,
        easing: 'easeOutQuint',
        step: function(now) {
            self._t_(now);
        },
        complete: function() {
            self.la = toDegrees;
            if (isInit) {
                self._sr_();
                return;
            }
            if (hasError) {
                if (self.swr && self.swr.ResponseCode == 8) {
                    self._iv_(true);
                    return;
                }
                self._ssw_();
                return;
            }
            if (!self.wp || (!_.isEmpty(self.wp) && self.wp.c == 0)) {
                self._sem_(self.t.wonNothing);
                self._isw_();
                return;
            }
            self._sp_(self.t.claimMessage);        
        }
    });
};

SW.prototype._ss_ = function(hasError) {
    var self = this,
        angle = 720 + (_.isEmpty(self.wp) || (!_.isEmpty(self.wp) && (self.wp.c === undefined || self.wp.c == 0)) ? 0 : self.wp.o);
    clearInterval(self.siid);
    self._ar_(self._gar_(), angle, 6000, hasError);
};

function _gp_() {
    sw.is = true;
    sw._rs_();
    sw._is_();
    sw._sw_();
}

SW.prototype._sw_ = function() {
    var self = this;
    if (!self.u) return;
    self._pswp_();
    self.swr = null;
    $.ajax({
        type: 'POST',
        async: true,
        url: '/api/rewards/spinwheel/spin',
        contentType: 'application/json',
        data: JSON.stringify(self.u),
        success: function(response) {
            try {
                if (!response) {
                    self._ss_(true);
                    return;
                }
                self.swr = response;
                setTimeout(function() {
                    self._gr_();
                }, 700);
            } catch (e) {
                self._ss_(true);
            }
        },
        error: function() {
            self._ss_(true);
        }
    });
};

SW.prototype._gr_ = function () {
    var self = this;
    if (!self.swr) {
        self._ss_(true);
        return;
    }
    self.wp = null;
    self.wp = _.find(self.swp, { c: self.swr.ResponseData.ProductCode });
    if (!self.wp)
        self.wp = _.find(amplify.store(self.sk), { c: self.swr.ResponseData.ProductCode });
    if (!self.wp) {
        self._ss_(true);
        return;
    } 
    self._ss_(); 
};

function _rp_() {
    $('#claimButton').hide();
    sw._ssr_(sw.t.successfulClaim);
}

SW.prototype._eb_ = function() {
    $('#spinButton').css('display', 'block');
    $('#spinButton').css('background', '#2a8fbd');
    $('#spinButton').attr('onclick', 'javascript: _gp_();');
    $('#spinButton').attr('href', '#');
    $('#spinButton').prop('disabled', false);
};

SW.prototype._db_ = function() {
    $('#spinButton').css('background', '#808080');
    $('#spinButton').attr('onclick', null);
    $('#spinButton').attr('href', null);
    $('#spinButton').prop('disabled', true);
};

SW.prototype._sp_ = function (message) {
    var self = this;
    message = message.replace('[prize]', self.wp.pn);
    var splitMessage = message.split('<br />').length > 1 ? message.split('<br />') : message.split('&lt;br /&gt;'),
        mobile = '<span>' + splitMessage[0] + '</span><br/><span>' + splitMessage[1] + '</span>',
        web = '<span>' + splitMessage[0] + '</span><span>' + splitMessage[1] + '</span>';
    $('#spinMessage').html(self.im ? mobile : web);
    self._dp_();
    $('#claimButton').css('display', 'block');
    $('#prizeModal').modal('show');
    self.is = false;
};

SW.prototype._ssr_ = function(message) {
    if (message.indexOf('html') !== -1) return;
    $('#spinMessage').html(message);
    $('#okButton').attr('onclick', 'javascript: _tp_(true);');
    $('#claimButton').hide();
    $('#okButton').css('display', 'block');
};

SW.prototype._sem_ = function (message, isInit) {
    var self = this;
    if(!message)
        message = !_.isEmpty(self.swr) && !_.isEmpty(self.swr.ResponseMessage) ? self.swr.ResponseMessage : self.t.message5; 
    $('#claimButton').hide();
    $('#okButton').css('display', 'block');
    $('#spinsLeft').html(message);
    $('#okButton').attr('onclick', 'javascript: _tp_();');
    self.is = false;
    if (!isInit) {
        $('#spinMessage').html(message);
        $('#prizeModal').modal('show');
        return;
    } 
    $('#spinsLeft').show();
    $('#spinWheelContent').show();  
};

function _tp_(isRedemption) {
    $('#prizeModal').modal('hide');
    $('#okButton').hide();
    var isUpdated = sw && (!_.isEmpty(sw.swr) && sw.swr.ResponseCode == 8);
    if (!isRedemption) {
        if (isUpdated) window.location.reload();
        return;
    }
    if (isUpdated) {
        sw.wp = null;
        setTimeout(function() {
            sw._iv_(true);
        }, 1000);
        return;
    }
    sw._ssw_(true);
}

SW.prototype._esw_ = function(isInit) {
    var self = this;
    if(self.swr && !_.isEmpty(self.swr.ResponseData))
        $('#spinsLeft').html(self.t.spinsLeftLabel1 + '<span>' + self.swr.ResponseData.Spins + '</span>' + self.t.spinsLeftLabel2);
    self._eb_();
    if (isInit) {
        document.getElementById('spinWheel').style.display = 'block';
    }
};

SW.prototype._ssw_ = function (isInit) {
    var self = this;
    self.hl = true;
    if (isInit) 
        $('#spinsLeft').show(); 
    if (self.swr && self.swr.ResponseCode != 0) {
        self._sem_(null, isInit);
        self._db_();
        $('#spinButton').hide();
        if (self.swr.ResponseCode == 1) {
            self._scd_(self.swr.ResponseData.Frequency);
        }
        return;
    }
    self._esw_(isInit);
};

SW.prototype._scd_ = function(frequency) {
    var self = this;
    $('#spinsLeft').html(self.swr.ResponseMessage);
    try {
        var serverDate = new Date(self.swr.ResponseData.ServerTime),
            serverTime = serverDate.getTime(),
            aMinuteInMillis = 1000 * 60,
            anHourInMillis = aMinuteInMillis * 60,
            aDayInMillis = anHourInMillis * 24;
        self.ciid = setInterval(function () {
            serverTime += 1000;
            var millisec;
            switch (frequency) {
                case 2:
                    millisec = (new Date(serverDate.getFullYear(), serverDate.getMonth(), serverDate.getDate(), 23, 59, 59)).getTime();
                    break;
                case 3:
                    millisec = (new Date(serverDate.getFullYear(), serverDate.getMonth(), serverDate.getDate() + (7 - serverDate.getDay()), 23, 59, 59)).getTime();
                    break;
                case 4:
                    millisec = (new Date(serverDate.getFullYear(), serverDate.getMonth() + 1, 0, 23, 59, 59)).getTime();
                    break;
            }
            var diff = millisec - serverTime;
            if (diff <= 0) {
                window.location.reload();
                clearInterval(self.ciid);
                return;
            }
            var days = Math.floor(diff / aDayInMillis),
                hours = Math.floor((diff / anHourInMillis) % 24),
                minutes = Math.floor((diff / aMinuteInMillis) % 60),
                secs = Math.floor((diff / 1000) % 60),
                cells = document.getElementById('countdownContainer').getElementsByTagName('div');
            for (var i = 0; i < cells.length - 1; i++) {
                switch (i) {
                    case 0:
                        cells[i].innerHTML = '<p>' + days + '</p><p>' + self.t.countdownDay + '</p>';
                        break;
                    case 1:
                        cells[i].innerHTML = '<p>' + hours + '</p><p>' + self.t.countdownHour + '</p>';
                        break;
                    case 2:
                        cells[i].innerHTML = '<p>' + minutes + '</p><p>' + self.t.countdownMin + '</p>';
                        break;
                    case 3:
                        cells[i].innerHTML = '<p>' + secs + '</p><p>' + self.t.countdownSec + '</p>';
                        break;
                }
            }
            if ($('#countdownContainer').css('display') === 'none') 
                $('#countdownContainer').show();         
        }, 1000);
    } catch (e) {}
};

SW.prototype._rs_ = function () {
    var self = this;
    self._db_();
    self.wp = {};
    self._cc_('pic');
    $('#spinMessage').html('');
    $('#okButton').hide();
    $('#claimButton').hide();
};

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}

SW.prototype._t_ = function(degrees) {
    var st = document.getElementById('spinWheel').style;
    if (st.hasOwnProperty('WebkitTransform') || st.constructor.prototype.hasOwnProperty('WebkitTransform')) {
        st['WebkitTransform'] = 'rotate(' + degrees + 'deg)';
    }
    if (st.hasOwnProperty('MozTransform') || st.constructor.prototype.hasOwnProperty('MozTransform')) {
        st['MozTransform'] = 'rotate(' + degrees + 'deg)';
    }
    if (!_.isEmpty(_.pick(st, 'msTransform'))) {
        st['msTransform'] = 'rotate(' + degrees + 'deg)';
    }
    if (st.hasOwnProperty('transform') || st.constructor.prototype.hasOwnProperty('transform')) {
        st['transform'] = 'rotate(' + degrees + 'deg)';
    }
};

SW.prototype._dp_ = function() {
    var self = this,
        c = document.getElementById('pic'),
        ctx = c.getContext('2d'),
        pi = new Image();
    ctx.canvas.width = self.pic.w;
    ctx.canvas.height = self.pic.h;
    ctx.save();
    if (!self.wp) return;
    pi.src = self.wp.is;
    pi.onload = function() {
        ctx.drawImage(pi, self.pic.a, self.pic.b);
        ctx.restore();
        ctx.save();
    };
};

SW.prototype._cc_ = function(id) {
    var c = document.getElementById(id),
        ctx = c.getContext('2d');
    ctx.clearRect(0, 0, c.width, c.height);
};

SW.prototype._gar_ = function() {
    var el = document.getElementById('spinWheel');
    var st = window.getComputedStyle(el, null);
    var tr = st.getPropertyValue('-webkit-transform') ||
        st.getPropertyValue('-moz-transform') ||
        st.getPropertyValue('-ms-transform') ||
        st.getPropertyValue('-o-transform') ||
        st.getPropertyValue('transform') ||
        st.getPropertyValue('msTransform') ||
        'FAIL';
    if (tr === 'FAIL') return 0;
    var values = tr.split('(')[1].split(')')[0].split(','),
        a = values[0],
        b = values[1],
        radians = Math.atan2(b, a);
    if (radians < 0) {
        radians += (2 * Math.PI);
    }
    var angle = Math.round(radians * (180 / Math.PI));
    return angle;
};

SW.prototype._gcd_ = function () {
    var self = this;
    self.swc = { h: $('#roulette').height(), w: $('#roulette').width() };
};

SW.prototype._rdc_ = function (shouldStore) {
    var self = this,
        c = document.getElementById('swc'),
        ctx = c.getContext('2d');
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    c = document.getElementById('pic'),
    ctx = c.getContext('2d'),
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
    $('#prizes').hide();
    setTimeout(function () {
        self._gcd_();
        self._gwc_();
        self._dsw_();
        self._dp_();
        $('#prizes').show();
    }, 500);
    if(shouldStore) 
        amplify.store(self.sk, self.swp);
};

SW.prototype._pswp_ = function () {
    var self = this,
        pswp = [],
        swp = amplify.store(self.sk);
    if (_.isEmpty(swp)) return;
    _.each(swp, function(p, i) {
        pswp.push({
            ImagePath: p.is,
            PrizeName: p.pn,
            ProductCode: p.c,
            PrizePercentage: 0
        });
    });
    self.u['CachedPrizeItems'] = pswp;
};