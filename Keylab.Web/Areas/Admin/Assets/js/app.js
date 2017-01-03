(function (win) {
    var utils = {
        ajax: function (options) {
            options.data || (options.data = {});
            options = $.extend({}, {
                url: "",
                data: {},
                //调试信息
                debug: false,
                async: false,
                succe: function (m) {
                    console.info('debug message');
                    console.log('\t', 'quest', options);
                    console.log('\t', 'ponse', m);
                }
            }, options);
            $.AMUI.progress && $.AMUI.progress.start();

            var aj = $.ajax({
                url: options.url,
                timeout: 5000,
                type: 'post',
                data: options.data,
                dataType: 'json',
                async: options.async,
                success: function (data) {
                    $.AMUI.progress && $.AMUI.progress.done();
                    if (options.debug) {
                        console.info('debug message');
                        console.log('\t', 'quest', options);
                        console.log('\t', 'ponse', data);
                    }
                    if (data.status == -1) {
                        setTimeout(function () {
                            utils.msgErr('用户信息验证失败,请重新登录', function () {
                                window.location.href = '/admin/login';
                            });
                        }, 2000);
                    } else {
                        options.succe && options.succe(data);
                    }
                },
                error: function (_, status) {
                    $.AMUI.progress && $.AMUI.progress.done();
                    console.log(status, _.responseText);
                    aj.abort();
                    if (status == 'timeout') {
                        utils.msgErr('灵图提示:<br/>网络超时');
                    } else {
                        utils.msgErr('灵图提示:<br/>网络似乎发生了意外');
                    }
                    //window.location.href="/"
                }
            });
        },

        /*
         * @description 综合信息提示
         * @param msg 提示信息
         * @param i 显示图标
         * @param call 回调函数
         */
        msg: function (msg, i, call) {
            if (i) {
                layer.msg(msg, { offset: '15%', icon: i, time: 2500 }, call);
            } else {
                layer.msg(msg, { offset: '15%', time: 2500 }, call);
            }
        },
        /*
         * @description 成功信息提示
         * @param msg 提示信息
         * @param i 显示图标
         * @param call 回调函数
         */
        msgOk: function (msg, call) {
            layer.msg(msg, { offset: '15%', icon: 6, time: 2000 }, call);
        },
        /*
         * @description 错误信息提示
         * @param msg 提示信息
         * @param call 回掉函数
         */
        msgErr: function (msg, call) {
            layer.msg(msg, { offset: '15%', icon: 5, time: 3000 }, call);
        },
        /*
         * @description 格式化两位 0 --> 00
         */
        bit2: function (v) {
            if (v < 10 || (v + '').length < 2) {
                return '0' + v;
            } else return v;
        },
        /*
         * @description 格式化日期 -->2016-08-01
         */
        fmtDate: function (v) {
            if (!v || v == '0001-01-01T00:00:00Z') return '';
            var dt = new Date(v);
            return dt.getFullYear() + '-' + this.bit2(dt.getMonth() + 1) + '-' + this.bit2(dt.getDate());
        },
        /*
         * @description 获取当前时间 ==>2016-08-01
         */
        getDate: function () {
            var dt = new Date();
            return dt.getFullYear() + '-' + this.bit2(dt.getMonth() + 1) + '-' + this.bit2(dt.getDate());
        },
        subStr: function (str, len) {
            if (str.length <= len) {
                return str;
            }
            return str.substr(0, len) + '...';
        },
        isURL: function (str_url) {
            var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
            + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //ftp的user@
            + "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184
            + "|" // 允许IP和DOMAIN（域名）
            + "([0-9a-z_!~*'()-]+\.)*" // 域名- www.
            + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名
            + "[a-z]{2,6})" // first level domain- .com or .museum
            + "(:[0-9]{1,4})?" // 端口- :80
            + "((/?)|" // a slash isn't required if there is no file name
            + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            var re = new RegExp(strRegex);
            return re.test(str_url)
        }
    }
    win.utils = utils;
})(window);


function login() {
    var $num = $("#num");
    var $pass = $("#pass");
    if ($num.val().trim() == '') {
        utils.msgErr('请输入账号');
        $num.focus();
        return
    }
    if ($pass.val().trim() == '') {
        utils.msgErr('请输入密码');
        $pass.focus();
        return
    }
    utils.ajax({
        url: "/admin/login/login",
        data: { num: $num.val().trim(), pass: md5($pass.val().trim()) },
        succe: function (data) {
            if (data.status == 1) {
                utils.msgOk('登陆成功', function () {
                    location.href = '/admin/index/index';
                });
            } else {
                utils.msgErr(data.message);
            }
        }
    });
}
