module.exports = {
    // 使用babel-eslint插件
    "parser": "babel-eslint",

    // 使用airbnb作为默认的eslint语法检查类型
    "extends": "airbnb",

    "env": {
        "browser": true,
        "node": true
    },
    "plugins": [
        "react",
        "jsx-a11y",
        "import"
    ],
    "rules": {
        "strict": 0,

        // 使用windows作为文件编辑器, 移除行尾LF的警告
        "linebreak-style": [2, "windows"]
    }
};