var share = {
    /**
    * 跨框架数据共享接口
    * @param {String} 存储的数据名
    * @param {Any} 将要存储的任意数据(无此项则返回被查询的数据)
    */
    data: function (name, value) {
        var top = window.top,
        cache = top['_CACHE'] || {};
        top['_CACHE'] = cache;
        return value ? cache[name] = value : cache[name];
    },
    /**
    * 数据共享删除接口
    * @param {String} 删除的数据名
    */
    removeData: function (name) {
        var cache = window.top['_CACHE'];
        if (cache && cache[name]) 
            delete cache[name];
    }
}; 