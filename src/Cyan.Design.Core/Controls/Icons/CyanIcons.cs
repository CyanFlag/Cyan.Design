using System.Collections.Generic;
using System.Linq;

namespace Cyan.Design.Core.Controls.Icons;

/// <summary>图标资源集合，提供图标键名到图标字符的映射</summary>
public static class CyanIcons
{
    private static readonly Dictionary<string, string> _map = new()
    {
        ["shangyi"] = "\ue966"
        ,
        ["bringtotop"] = "\ue967"
        ,
        ["yuanfucengfenxiang"] = "\ue968"
        ,
        ["louceng"] = "\ue969"
        ,
        ["toushi"] = "\ue96a"
        ,
        ["fushi"] = "\ue96b"
        ,
        ["fushi1"] = "\ue96c"
        ,
        ["fushi2"] = "\ue96d"
        ,
        ["tuceng"] = "\ue96e"
        ,
        ["shangyi1"] = "\ue96f"
        ,
        ["fuhao-tuceng"] = "\ue970"
        ,
        ["control-up"] = "\ue971"
        ,
        ["dixingtushujuruku"] = "\ue972"
        ,
        ["shangyi2"] = "\ue974"
        ,
        ["shitu"] = "\ue975"
        ,
        ["ic_terrain"] = "\ue976"
        ,
        ["wanggeguan"] = "\ue977"
        ,
        ["wanggekai"] = "\ue978"
        ,
        ["shangyi3"] = "\ue979"
        ,
        ["toushi1"] = "\ue97a"
        ,
        ["tuceng1"] = "\ue97b"
        ,
        ["24gl-appsSmall2"] = "\ue9b9"
        ,
        ["24gl-appsSmall4"] = "\ue9ba"
        ,
        ["24gl-appsSmall3"] = "\ue9bb"
        ,
        ["24gl-appsSmall"] = "\ue9bc"
        ,
        ["24gf-appsSmall3"] = "\ue9c0"
        ,
        ["24gf-appsSmall"] = "\ue9c1"
        ,
        ["24gf-appsSmall4"] = "\ue9c2"
        ,
        ["cengmianfangxiang"] = "\ue97c"
        ,
        ["paichudingcengtuxing"] = "\ue97d"
        ,
        ["toushitu"] = "\ue97e"
        ,
        ["zihuidixing1"] = "\ue97f"
        ,
        ["terrain_px"] = "\ue980"
        ,
        ["terrain_px1"] = "\ue981"
        ,
        ["tuceng2"] = "\ue982"
        ,
        ["view"] = "\ue983"
        ,
        ["layer"] = "\ue984"
        ,
        ["terrain_px2"] = "\ue985"
        ,
        ["terrain_px3"] = "\uea91"
        ,
        ["icon-test50"] = "\ue9df"
        ,
        ["shangyi-copy"] = "\ue987"
        ,
        ["zhiyudiceng"] = "\ue988"
        ,
        ["ICONbrandAllTerrain"] = "\ue989"
        ,
        ["baseline-terrain-px"] = "\ue99e"
        ,
        ["shangyi4"] = "\ue98a"
        ,
        ["shituxinanshitu"] = "\ue98b"
        ,
        ["shituhoushitu"] = "\ue98c"
        ,
        ["shituyangshitu"] = "\ue98d"
        ,
        ["shituzhushitu"] = "\ue98e"
        ,
        ["shituyoushitu"] = "\ue98f"
        ,
        ["shitufushitu"] = "\ue990"
        ,
        ["shituzuoshitu"] = "\ue992"
        ,
        ["shitushiti"] = "\ue993"
        ,
        ["shitutoushi"] = "\ue994"
        ,
        ["cengji"] = "\ue995"
        ,
        ["yinqing_tuceng"] = "\ue996"
        ,
        ["tuceng3"] = "\ue997"
        ,
        ["fushi3"] = "\ue998"
        ,
        ["terrain_rounded"] = "\ue99a"
        ,
        ["wangge"] = "\ue99b"
        ,
        ["bim-model-terrain"] = "\ue99c"
        ,
        ["gis_shendujiancetoushi"] = "\ue99d"
        ,
        ["tuceng4"] = "\ue99f"
        ,
        ["3dterrain"] = "\ue9a0"
        ,
        ["ic_terrain1"] = "\ue9a1"
        ,
        ["terrain1"] = "\ue9a2"
        ,
        ["shangyi5"] = "\ue9a3"
        ,
        ["perspective"] = "\ue9a4"
        ,
        ["terrain_24px_outlined"] = "\uec9f"
        ,
        ["dixingruku"] = "\ue9a5"
        ,
        ["shitutubiao_shituliandong"] = "\ue9a6"
        ,
        ["md-terrain"] = "\ue9a7"
        ,
        ["SX_003"] = "\ue9a8"
        ,
        ["SX_004"] = "\ue9a9"
        ,
        ["wangge1"] = "\ue9aa"
        ,
        ["toushitu1"] = "\ue9ab"
        ,
        ["zanwutuceng"] = "\ue9ac"
        ,
        ["terrain-analysis"] = "\ue9b1"
        ,
        ["shitulei"] = "\ue9b2"
        ,
        ["a-3Dtoushi"] = "\ue9b3"
        ,
        ["fushitu"] = "\ue9b4"
        ,
        ["terrain2"] = "\ue9b5"
        ,
        ["tuceng5"] = "\ue9b6"
        ,
        ["tuceng6"] = "\ue9b7"
        ,
        ["fushitu1"] = "\ue9b8"
        ,
        ["tuceng7"] = "\ue9bd"
        ,
        ["-_yincangdixing"] = "\ue9be"
        ,
        ["-_3Dshitu"] = "\ue9bf"
        ,
        ["-_yincangdixing1"] = "\ue9c3"
        ,
        ["cengji1"] = "\ue9c4"
        ,
        ["wangge2"] = "\ue9c5"
        ,
        ["toushitu2"] = "\ue9c6"
        ,
        ["fushitu2"] = "\ue9c7"
        ,
        ["TerrainMatchSides"] = "\ue9c8"
        ,
        ["terrain-16"] = "\ue9c9"
        ,
        ["a-shitufushitu"] = "\ue9cb"
        ,
        ["a-shituzuoshitu"] = "\ue9cc"
        ,
        ["a-shituhoushitu"] = "\ue9cd"
        ,
        ["a-shituyangshitu"] = "\ue9ce"
        ,
        ["shitu1"] = "\ue9d0"
        ,
        ["wangge3"] = "\ue9d1"
        ,
        ["Abstract_lititoushi_stereo-perspective"] = "\ue9d2"
        ,
        ["Edit_toushi_perspective_linear"] = "\ue9d3"
        ,
        ["Edit_toushi_perspective_linear1"] = "\ue9d5"
        ,
        ["Abstract_lititoushi_stereo-perspective1"] = "\ue9d6"
        ,
        ["haze1"] = "\ue9d7"
        ,
        ["helicopter"] = "\ue9d9"
        ,
        ["hospital"] = "\ue9da"
        ,
        ["hop"] = "\ue9db"
        ,
        ["land-plot"] = "\ue9dc"
        ,
        ["leaf"] = "\ue9dd"
        ,
        ["syringe"] = "\ue9de"
        ,
        ["tornado"] = "\ue9e0"
        ,
        ["trees"] = "\ue9e1"
        ,
        ["tree-palm"] = "\ue9e2"
        ,
        ["snowflake"] = "\ue9e3"
        ,
        ["tent-tree"] = "\ue9e4"
        ,
        ["model-terrain"] = "\ue9e5"
        ,
        ["terrain3"] = "\ue9e6"
        ,
        ["align-end-horizontal"] = "\ue9e7"
        ,
        ["align-horizontal-justify-end"] = "\ue9e8"
        ,
        ["align-end-vertical"] = "\ue9e9"
        ,
        ["anvil"] = "\ue9ea"
        ,
        ["armchair"] = "\ue9eb"
        ,
        ["arrow-big-up-dash"] = "\ue9ec"
        ,
        ["arrow-down-wide-narrow"] = "\ue9ed"
        ,
        ["arrow-down-to-line"] = "\ue9ef"
        ,
        ["arrow-up-to-line"] = "\ue9f0"
        ,
        ["arrow-down-from-line"] = "\ue9f1"
        ,
        ["arrow-up-narrow-wide"] = "\ue9f2"
        ,
        ["arrow-down-narrow-wide"] = "\ue9f3"
        ,
        ["arrow-up-wide-narrow"] = "\ue9f4"
        ,
        ["chess-rook"] = "\ue9f5"
        ,
        ["chess-queen"] = "\ue9f6"
        ,
        ["cigarette"] = "\ue9f7"
        ,
        ["chess-king"] = "\ue9f8"
        ,
        ["cloud-backup"] = "\ue9f9"
        ,
        ["cloud-drizzle"] = "\ue9fa"
        ,
        ["cloud-hail"] = "\ue9fb"
        ,
        ["clock-alert"] = "\ue9fc"
        ,
        ["cloud-check"] = "\ue9fd"
        ,
        ["cherry"] = "\ue9fe"
        ,
        ["cloud-fog"] = "\ue9ff"
        ,
        ["cloud-snow"] = "\uea00"
        ,
        ["cloud-rain"] = "\uea01"
        ,
        ["cloud-moon"] = "\uea02"
        ,
        ["cloud-rain-wind"] = "\uea03"
        ,
        ["church"] = "\uea04"
        ,
        ["construction"] = "\uea05"
        ,
        ["crown"] = "\uea07"
        ,
        ["brush"] = "\uea08"
        ,
        ["dice-2"] = "\uea09"
        ,
        ["dice-5"] = "\uea0a"
        ,
        ["dice-6"] = "\uea0b"
        ,
        ["dice-4"] = "\uea0c"
        ,
        ["dice-1"] = "\uea0d"
        ,
        ["dog"] = "\uea0e"
        ,
        ["drama"] = "\uea0f"
        ,
        ["candy"] = "\uea10"
        ,
        ["egg"] = "\uea11"
        ,
        ["egg-off"] = "\uea12"
        ,
        ["fence"] = "\uea13"
        ,
        ["fish"] = "\uea14"
        ,
        ["flower-2"] = "\uea15"
        ,
        ["frame"] = "\uea16"
        ,
        ["grape"] = "\uea17"
        ,
        ["ham"] = "\uea18"
        ,
        ["helicopter1"] = "\uea19"
        ,
        ["key-square"] = "\uea1a"
        ,
        ["landmark"] = "\uea1b"
        ,
        ["lightbulb"] = "\uea1c"
        ,
        ["line-squiggle"] = "\uea1d"
        ,
        ["leaf1"] = "\uea1e"
        ,
        ["leafy-green"] = "\uea1f"
        ,
        ["magnet"] = "\uea20"
        ,
        ["flame-kindling"] = "\uea21"
        ,
        ["fish-symbol"] = "\uea22"
        ,
        ["flag-off"] = "\uea23"
        ,
        ["flask-conical"] = "\uea24"
        ,
        ["milestone"] = "\uea25"
        ,
        ["mountain-snow"] = "\uea26"
        ,
        ["mountain"] = "\uea27"
        ,
        ["motorbike"] = "\uea28"
        ,
        ["haze2"] = "\uea29"
        ,
        ["package-minus"] = "\uea2a"
        ,
        ["origami"] = "\uea2b"
        ,
        ["option"] = "\uea2c"
        ,
        ["hospital1"] = "\uea2e"
        ,
        ["ice-cream-bowl"] = "\uea2f"
        ,
        ["pickaxe"] = "\uea30"
        ,
        ["piggy-bank"] = "\uea31"
        ,
        ["popsicle"] = "\uea32"
        ,
        ["popcorn"] = "\uea33"
        ,
        ["puzzle"] = "\uea34"
        ,
        ["rabbit"] = "\uea35"
        ,
        ["road"] = "\uea36"
        ,
        ["luggage"] = "\uea37"
        ,
        ["ribbon"] = "\uea38"
        ,
        ["roller-coaster"] = "\uea39"
        ,
        ["rose"] = "\uea3a"
        ,
        ["rocking-chair"] = "\uea3b"
        ,
        ["scale"] = "\uea3c"
        ,
        ["scooter"] = "\uea3d"
        ,
        ["section"] = "\uea3e"
        ,
        ["shovel"] = "\uea3f"
        ,
        ["signpost-big"] = "\uea40"
        ,
        ["nut"] = "\uea41"
        ,
        ["slice"] = "\uea42"
        ,
        ["snail"] = "\uea43"
        ,
        ["sofa"] = "\uea44"
        ,
        ["solar-panel"] = "\uea45"
        ,
        ["sparkle"] = "\uea46"
        ,
        ["sparkles"] = "\uea47"
        ,
        ["paint-bucket"] = "\uea48"
        ,
        ["pencil-ruler"] = "\uea49"
        ,
        ["star-half"] = "\uea4a"
        ,
        ["saudi-riyal"] = "\uea4b"
        ,
        ["swatch-book"] = "\uea4c"
        ,
        ["salad"] = "\uea4d"
        ,
        ["shrimp"] = "\uea4e"
        ,
        ["tree-palm1"] = "\uea4f"
        ,
        ["turtle"] = "\uea50"
        ,
        ["vegan"] = "\uea51"
        ,
        ["squirrel"] = "\uea52"
        ,
        ["wand-sparkles"] = "\uea53"
        ,
        ["wheat"] = "\uea54"
        ,
        ["tractor"] = "\uea55"
        ,
        ["zodiac-aries"] = "\uea56"
        ,
        ["zodiac-leo"] = "\uea57"
        ,
        ["wind-arrow-down"] = "\uea58"
        ,
        ["shangyiyiceng"] = "\ue7fe"
        ,
        ["xiayiyiceng"] = "\ue803"
        ,
        ["shuiyun_shenshuiguoduheduanxinxi"] = "\ue815"
        ,
        ["heliu"] = "\ue843"
        ,
        ["heliu-"] = "\ue8f4"
        ,
        ["shuishen"] = "\ue906"
        ,
        ["heliu1"] = "\ue908"
        ,
        ["xiangxiayiceng"] = "\ue93b"
        ,
        ["xiangshangyiceng"] = "\ue93c"
        ,
        ["xiayiyiceng1"] = "\ue93d"
        ,
        ["shangyiyiceng1"] = "\ue93e"
        ,
        ["shangyiyiceng2"] = "\ue93f"
        ,
        ["shuiwei"] = "\uea2d"
        ,
        ["xiayiyiceng2"] = "\ue940"
        ,
        ["shangyiyiceng11"] = "\ue941"
        ,
        ["shangyiyiceng21"] = "\ue942"
        ,
        ["xiayiyiceng21"] = "\ue943"
        ,
        ["xiayiyiceng11"] = "\ue944"
        ,
        ["xiayiyiceng3"] = "\ue945"
        ,
        ["xiayiyiceng4"] = "\ue946"
        ,
        ["shangyiyiceng3"] = "\ue947"
        ,
        ["shangyiceng-01"] = "\ue948"
        ,
        ["tucengxiayiyiceng--mianxing"] = "\ue949"
        ,
        ["icon_shangyiyiceng"] = "\ue94a"
        ,
        ["xiayiyiceng12"] = "\ue94b"
        ,
        ["shangyiyiceng4"] = "\ue94c"
        ,
        ["a-5shangyiceng"] = "\ue94d"
        ,
        ["shangyiyiceng5"] = "\ue94e"
        ,
        ["shangyiyiceng6"] = "\ue94f"
        ,
        ["xiayiyiceng5"] = "\ue950"
        ,
        ["shangyiyiceng7"] = "\ue951"
        ,
        ["shangyiyiceng8"] = "\ue952"
        ,
        ["tuceng_xiayiyiceng"] = "\ue954"
        ,
        ["shangyiceng"] = "\ue955"
        ,
        ["shangyiyiceng-1"] = "\ue956"
        ,
        ["tubiaokuzhizuo_shangyiyiceng"] = "\ue957"
        ,
        ["tubiaokuzhizuo_xiayiyiceng"] = "\ue958"
        ,
        ["hedao"] = "\ue959"
        ,
        ["xiayiyiceng6"] = "\ue95a"
        ,
        ["xiayiyiceng7"] = "\ue95b"
        ,
        ["shangyiyiceng9"] = "\ue95c"
        ,
        ["xiayiyiceng8"] = "\ue95d"
        ,
        ["xiayiyiceng9"] = "\ue95e"
        ,
        ["shangyiyiceng10"] = "\ue95f"
        ,
        ["xiayiyiceng10"] = "\ue960"
        ,
        ["heliu2"] = "\ueb18"
        ,
        ["shangyiyiceng12"] = "\ue961"
        ,
        ["xiayiyiceng13"] = "\ue962"
        ,
        ["icon-s-shangyiyiceng"] = "\ue963"
        ,
        ["shangyiyiceng13"] = "\ue964"
        ,
        ["yidongdaoxiayiceng-o"] = "\ue965"
        ,
        ["futou1"] = "\ue801"
        ,
        ["minjie"] = "\ue903"
        ,
        ["minjiehua"] = "\ue802"
        ,
        ["zhili1"] = "\ue904"
        ,
        ["fl-futou"] = "\ue804"
        ,
        ["dimian6"] = "\ue9ee"
        ,
        ["icondesign-"] = "\ue805"
        ,
        ["dimianjiangshui"] = "\ue905"
        ,
        ["bishou"] = "\ue806"
        ,
        ["a-bishou1x"] = "\ue909"
        ,
        ["jirouliliang"] = "\ue807"
        ,
        ["danfeng"] = "\ue90a"
        ,
        ["zhilifayu"] = "\ue808"
        ,
        ["duofeng"] = "\ue90b"
        ,
        ["shiwu-chuizi"] = "\ue809"
        ,
        ["sepu"] = "\ue90c"
        ,
        ["baofahuonailixuanshou"] = "\ue80a"
        ,
        ["ganraofeng"] = "\ue90d"
        ,
        ["dimianpingmian"] = "\ue80b"
        ,
        ["gongliuchufeng"] = "\ue90e"
        ,
        ["dimiantuqi"] = "\ue80c"
        ,
        ["pinghua1"] = "\ue90f"
        ,
        ["jian4"] = "\ue80d"
        ,
        ["jiangzao"] = "\ue910"
        ,
        ["dimian"] = "\ue80e"
        ,
        ["tongfenyigouti"] = "\ue912"
        ,
        ["icon_wd_thsh"] = "\ue80f"
        ,
        ["xuanfeng"] = "\ue913"
        ,
        ["dimian1"] = "\ue810"
        ,
        ["duogefengxing"] = "\ue914"
        ,
        ["dimian2"] = "\ue811"
        ,
        ["xianshidimianwangge1-01"] = "\uebd6"
        ,
        ["weibiaoti-"] = "\ue812"
        ,
        ["dixing_line"] = "\ue915"
        ,
        ["jijian"] = "\ue813"
        ,
        ["dimianfengshibiao"] = "\ue916"
        ,
        ["jian5"] = "\uead0"
        ,
        ["a-bishoudaowuqizhanzheng"] = "\ue917"
        ,
        ["select"] = "\ue814"
        ,
        ["a-bishouwuqizhanzheng"] = "\ue918"
        ,
        ["sanbanfu"] = "\ue816"
        ,
        ["dixingxianying"] = "\ue919"
        ,
        ["jianyu"] = "\ue817"
        ,
        ["tubiao2-05"] = "\ue91a"
        ,
        ["fanghudunpaianquan"] = "\ue818"
        ,
        ["dimian7"] = "\ue91b"
        ,
        ["pinghuadingdi"] = "\ue81a"
        ,
        ["fenleidixing1"] = "\ue91c"
        ,
        ["pen"] = "\ue81c"
        ,
        ["armour"] = "\ue91d"
        ,
        ["futou2"] = "\ue81d"
        ,
        ["a-bishoudaowuqizhanzheng1"] = "\ue91e"
        ,
        ["24gl-shield"] = "\ue9d4"
        ,
        ["a-bishouwuqizhanzheng1"] = "\ue91f"
        ,
        ["24gf-shield"] = "\ue9d8"
        ,
        ["a-zhibeidixing1x"] = "\ue921"
        ,
        ["terrain"] = "\ue81e"
        ,
        ["dimianchenjiang1"] = "\ue922"
        ,
        ["jingshenxinlike"] = "\ue81f"
        ,
        ["a-bishoudao"] = "\ue923"
        ,
        ["wulianwangganzhi"] = "\ue820"
        ,
        ["jian11"] = "\ue924"
        ,
        ["ditudiejiafenxi-"] = "\ue821"
        ,
        ["jiegoupinghua"] = "\ue925"
        ,
        ["jian6"] = "\ue822"
        ,
        ["dimian8"] = "\ue926"
        ,
        ["zhiliyouxi"] = "\ue823"
        ,
        ["dixingbianji2"] = "\uecb9"
        ,
        ["zhandimianji"] = "\ue824"
        ,
        ["icon_dixingwadong"] = "\ue927"
        ,
        ["pingzhengdimian"] = "\ue825"
        ,
        ["tiedimian"] = "\ue928"
        ,
        ["jingshenke"] = "\ue827"
        ,
        ["yizhidixingtu"] = "\ue929"
        ,
        ["jian7"] = "\ue828"
        ,
        ["yizhidixingtu1"] = "\ue92a"
        ,
        ["dunpaitubiao"] = "\ue829"
        ,
        ["dixing12"] = "\ue92b"
        ,
        ["icon-smoothing-tools"] = "\ue82a"
        ,
        ["a-TerrainLayer"] = "\ue92c"
        ,
        ["jingshenke1"] = "\ue82b"
        ,
        ["mti-dimianchenjiang"] = "\ue92d"
        ,
        ["pinghuahuise"] = "\ue82c"
        ,
        ["mti-dimiantaxiandian"] = "\ue92e"
        ,
        ["dimianyaping"] = "\ue82d"
        ,
        ["tiedimianji1"] = "\ue92f"
        ,
        ["jian8"] = "\ue82e"
        ,
        ["pinghua2"] = "\ue930"
        ,
        ["icon-test49"] = "\ue82f"
        ,
        ["dimianchenjiang2"] = "\ue931"
        ,
        ["pinghuadimian"] = "\ue830"
        ,
        ["zhili2"] = "\ue932"
        ,
        ["pinghuadimian1"] = "\ue831"
        ,
        ["minjie1"] = "\ue933"
        ,
        ["dunpai3"] = "\ue832"
        ,
        ["a-shuijindimian"] = "\ue934"
        ,
        ["dimian3"] = "\ue833"
        ,
        ["a-TerrainFlattening"] = "\ue935"
        ,
        ["gongjushiyongfangfa"] = "\ue834"
        ,
        ["a-TerrainFlattening1"] = "\ue936"
        ,
        ["liliang"] = "\ue835"
        ,
        ["a-TerrainFlattening2"] = "\ue937"
        ,
        ["dimiantaxian"] = "\ue836"
        ,
        ["dixingqifudu1"] = "\uea6b"
        ,
        ["dimianchenjiang"] = "\ue837"
        ,
        ["dixingbiaogao"] = "\ue938"
        ,
        ["daorudimian"] = "\ue953"
        ,
        ["dixing13"] = "\ue939"
        ,
        ["pinghuaquxian"] = "\ue838"
        ,
        ["dimian9"] = "\ue93a"
        ,
        ["type-3-subtype-700"] = "\ue839"
        ,
        ["dixingfenxi"] = "\ue83a"
        ,
        ["dimianshidu"] = "\ue83b"
        ,
        ["line-pencil"] = "\ue8dd"
        ,
        ["icon_nailipao"] = "\ue83c"
        ,
        ["guangjian"] = "\ue83d"
        ,
        ["dixingqifudu"] = "\ue83e"
        ,
        ["dun4"] = "\ue83f"
        ,
        ["dixingbianji"] = "\ue840"
        ,
        ["zhandimianji1"] = "\ue841"
        ,
        ["mugongfu"] = "\ue842"
        ,
        ["dimianjiejing"] = "\uec9a"
        ,
        ["jingshenke2"] = "\ue844"
        ,
        ["dixingtu1"] = "\ue845"
        ,
        ["044chuizi"] = "\ue87d"
        ,
        ["shield"] = "\ue875"
        ,
        ["shield-full"] = "\ue876"
        ,
        ["judge"] = "\ue879"
        ,
        ["judge-full"] = "\ue87a"
        ,
        ["nailipao"] = "\ue8f1"
        ,
        ["body_armor"] = "\uef08"
        ,
        ["dimian4"] = "\ue846"
        ,
        ["shiyu"] = "\ue847"
        ,
        ["fayuyuzhili"] = "\ue87b"
        ,
        ["yizhidingceng"] = "\ue848"
        ,
        ["jiezu"] = "\ue849"
        ,
        ["fenzu"] = "\ue84a"
        ,
        ["jiaoji"] = "\ue84b"
        ,
        ["chaji"] = "\ue84c"
        ,
        ["lianji"] = "\ue84d"
        ,
        ["zuoyoufanzhuan"] = "\ue84e"
        ,
        ["xuanzhuan"] = "\ue84f"
        ,
        ["ic_bj"] = "\ue850"
        ,
        ["edit2"] = "\ue851"
        ,
        ["zhili"] = "\ue852"
        ,
        ["kuijia1"] = "\ue853"
        ,
        ["celsius-line"] = "\ue854"
        ,
        ["blaze-line"] = "\ue855"
        ,
        ["cloudy-2-line"] = "\ue856"
        ,
        ["cloud-windy-line"] = "\ue857"
        ,
        ["cloudy-line"] = "\ue858"
        ,
        ["blaze-fill"] = "\ue859"
        ,
        ["fahrenheit-fill"] = "\ue85a"
        ,
        ["flashlight-fill"] = "\ue85b"
        ,
        ["fahrenheit-line"] = "\ue85c"
        ,
        ["foggy-line"] = "\ue85d"
        ,
        ["earthquake-line"] = "\ue85e"
        ,
        ["celsius-fill"] = "\ue85f"
        ,
        ["drizzle-line"] = "\ue860"
        ,
        ["cloudy-2-fill"] = "\ue861"
        ,
        ["fire-line"] = "\ue862"
        ,
        ["hail-line"] = "\ue863"
        ,
        ["heavy-showers-fill"] = "\ue864"
        ,
        ["flood-line"] = "\ue865"
        ,
        ["hail-fill"] = "\ue866"
        ,
        ["mist-fill"] = "\ue867"
        ,
        ["mist-line"] = "\ue868"
        ,
        ["heavy-showers-line"] = "\ue869"
        ,
        ["haze-fill"] = "\ue86a"
        ,
        ["foggy-fill"] = "\ue86b"
        ,
        ["meteor-line"] = "\ue86c"
        ,
        ["moon-clear-line"] = "\ue86d"
        ,
        ["moon-cloudy-fill"] = "\ue86e"
        ,
        ["moon-foggy-line"] = "\ue86f"
        ,
        ["haze-2-fill"] = "\ue870"
        ,
        ["moon-line"] = "\ue871"
        ,
        ["rainbow-line"] = "\ue872"
        ,
        ["cloudy-fill"] = "\ue873"
        ,
        ["earthquake-fill"] = "\ue874"
        ,
        ["moon-cloudy-line"] = "\ue877"
        ,
        ["moon-foggy-fill"] = "\ue878"
        ,
        ["showers-line"] = "\ue87c"
        ,
        ["drizzle-fill"] = "\ue87e"
        ,
        ["moon-fill"] = "\ue87f"
        ,
        ["snowy-fill"] = "\ue880"
        ,
        ["temp-cold-fill"] = "\ue881"
        ,
        ["flashlight-line"] = "\ue882"
        ,
        ["showers-fill"] = "\ue883"
        ,
        ["sun-line"] = "\ue884"
        ,
        ["rainbow-fill"] = "\ue885"
        ,
        ["sun-cloudy-line"] = "\ue886"
        ,
        ["sun-fill"] = "\ue887"
        ,
        ["rainy-fill"] = "\ue888"
        ,
        ["haze-line"] = "\ue889"
        ,
        ["tornado-fill"] = "\ue88a"
        ,
        ["temp-hot-line"] = "\ue88b"
        ,
        ["windy-line"] = "\ue88c"
        ,
        ["sun-foggy-line"] = "\ue88d"
        ,
        ["typhoon-fill"] = "\ue88e"
        ,
        ["sun-foggy-fill"] = "\ue88f"
        ,
        ["ancient-pavilion-fill"] = "\ue890"
        ,
        ["thunderstorms-fill"] = "\ue891"
        ,
        ["rainy-line"] = "\ue892"
        ,
        ["temp-cold-line"] = "\ue893"
        ,
        ["snowy-line"] = "\ue894"
        ,
        ["thunderstorms-line"] = "\ue895"
        ,
        ["ancient-gate-fill"] = "\ue896"
        ,
        ["typhoon-line"] = "\ue897"
        ,
        ["haze-2-line"] = "\ue898"
        ,
        ["building-2-line"] = "\ue899"
        ,
        ["bank-fill"] = "\ue89a"
        ,
        ["tornado-line"] = "\ue89b"
        ,
        ["building-3-line"] = "\ue89c"
        ,
        ["send-plane-fill"] = "\ue89d"
        ,
        ["inbox-archive-fill"] = "\ue89e"
        ,
        ["magic-line"] = "\ue89f"
        ,
        ["mark-pen-line"] = "\ue8a0"
        ,
        ["paint-line"] = "\ue8a1"
        ,
        ["mark-pen-fill"] = "\ue8a2"
        ,
        ["paint-brush-line"] = "\ue8a3"
        ,
        ["pen-nib-fill"] = "\ue8a4"
        ,
        ["pantone-fill"] = "\ue8a5"
        ,
        ["pantone-line"] = "\ue8a6"
        ,
        ["ruler-line"] = "\ue8a7"
        ,
        ["shape-fill"] = "\ue8a8"
        ,
        ["braces-fill"] = "\ue8a9"
        ,
        ["shape-2-fill"] = "\ue8aa"
        ,
        ["code-line"] = "\ue8ab"
        ,
        ["code-s-slash-fill"] = "\ue8ac"
        ,
        ["git-branch-line"] = "\ue8ad"
        ,
        ["code-s-slash-line"] = "\ue8ae"
        ,
        ["code-fill"] = "\ue8af"
        ,
        ["git-merge-fill"] = "\ue8b0"
        ,
        ["git-merge-line"] = "\ue8b1"
        ,
        ["command-line"] = "\ue8b2"
        ,
        ["git-pull-request-line"] = "\ue8b3"
        ,
        ["dixing10"] = "\ue8b4"
        ,
        ["yueduye-shezhi-huyanmoshi-2"] = "\ue8b5"
        ,
        ["ic-line-pencil"] = "\ue8b6"
        ,
        ["jingshenjingshenjibingjingshenjiankang11"] = "\ue8b7"
        ,
        ["2D"] = "\ue8b8"
        ,
        ["3D"] = "\ue8b9"
        ,
        ["dixingfenxi-caodi"] = "\ue8ba"
        ,
        ["dixingfenxi-lindi"] = "\ue8bb"
        ,
        ["rectangle"] = "\ue8bc"
        ,
        ["General-2-71"] = "\ue8bd"
        ,
        ["dimian5"] = "\ue8be"
        ,
        ["dixingbianji1"] = "\ue8bf"
        ,
        ["dixingxinxi"] = "\ue8c0"
        ,
        ["dixingtuxiazai"] = "\ue8c1"
        ,
        ["yidimianji"] = "\uee39"
        ,
        ["ico_dixingbianji"] = "\ue8c2"
        ,
        ["blizzard"] = "\ue8c3"
        ,
        ["cloudy"] = "\ue8c4"
        ,
        ["dust"] = "\ue8c5"
        ,
        ["a-cloudyatnight"] = "\ue8c6"
        ,
        ["haze"] = "\ue8c7"
        ,
        ["a-great-heavysnow"] = "\ue8c8"
        ,
        ["fog"] = "\ue8c9"
        ,
        ["a-extraordinaryrainstorm"] = "\ue8ca"
        ,
        ["a-heavyrain"] = "\ue8cb"
        ,
        ["a-heavysmog"] = "\ue8cc"
        ,
        ["a-heavyrainstorm"] = "\ue8cd"
        ,
        ["a-lightrain"] = "\ue8ce"
        ,
        ["a-mediumsnow"] = "\ue8cf"
        ,
        ["a-lighthaze"] = "\ue8d0"
        ,
        ["a-heavysnow"] = "\ue8d1"
        ,
        ["a-moderatesmog"] = "\ue8d2"
        ,
        ["moon"] = "\ue8d4"
        ,
        ["a-moderaterain"] = "\ue8d5"
        ,
        ["a-nightshowers"] = "\ue8d6"
        ,
        ["sandstorm"] = "\ue8d7"
        ,
        ["a-sandblowing"] = "\ue8d8"
        ,
        ["rainstorm"] = "\ue8d9"
        ,
        ["a-slightsnow"] = "\ue8da"
        ,
        ["sleet_02"] = "\ue8db"
        ,
        ["sleet_01"] = "\ue8dc"
        ,
        ["a-partlycloudy_01"] = "\ue8de"
        ,
        ["a-snowshowersatnight"] = "\ue8e2"
        ,
        ["a-showerrain"] = "\ue8e3"
        ,
        ["thunder"] = "\ue8e4"
        ,
        ["thunderstorm"] = "\ue8e5"
        ,
        ["a-Strongduststorm"] = "\ue8e6"
        ,
        ["snow"] = "\ue8e7"
        ,
        ["sunny"] = "\ue8e8"
        ,
        ["a-partlycloudy"] = "\ue8e9"
        ,
        ["a-superblizzard"] = "\ue8ea"
        ,
        ["a-snowshowers"] = "\ue8eb"
        ,
        ["bianji1"] = "\ue8ec"
        ,
        ["pinghuaquxian1"] = "\ue8ed"
        ,
        ["dimianzhankai"] = "\ue8ee"
        ,
        ["minjiekaifa"] = "\ue8ef"
        ,
        ["zhandimianji2"] = "\ue8f0"
        ,
        ["tiehedimian"] = "\ue8f2"
        ,
        ["dixingwadi1"] = "\ue8f3"
        ,
        ["a-chuizishenpan"] = "\ue8f5"
        ,
        ["dimiantaxian1"] = "\ue8f6"
        ,
        ["dixing11"] = "\ue8f7"
        ,
        ["fuwen"] = "\ued33"
        ,
        ["zhifubao"] = "\ue7ef"
        ,
        ["pencil"] = "\ue8f8"
        ,
        ["dacumojian"] = "\ue7f0"
        ,
        ["lvdi-mianxing"] = "\ue8f9"
        ,
        ["jingshen"] = "\ue7f3"
        ,
        ["gangling31"] = "\ue8fa"
        ,
        ["jingshen1"] = "\ue7f4"
        ,
        ["magic-wand"] = "\ue8fb"
        ,
        ["hudun"] = "\ue7f5"
        ,
        ["helmet-battle"] = "\ue907"
        ,
        ["daofu"] = "\ue7f7"
        ,
        ["tiedimianji"] = "\ue8fc"
        ,
        ["daojianfu"] = "\ue7f9"
        ,
        ["-_dimianxian-21"] = "\ue8fd"
        ,
        ["fatie"] = "\ue7fa"
        ,
        ["jian10"] = "\ue911"
        ,
        ["futou"] = "\ue7fb"
        ,
        ["lizhiicon_zhilicanji"] = "\ue8fe"
        ,
        ["strong"] = "\ue7fc"
        ,
        ["lindimianji"] = "\ue8ff"
        ,
        ["dunpai1"] = "\ue7ff"
        ,
        ["pinghua"] = "\ue900"
        ,
        ["sen015"] = "\ue800"
        ,
        ["liliang1"] = "\ue901"
        ,
        ["shu1"] = "\ue616"
        ,
        ["shu2"] = "\ue61a"
        ,
        ["home"] = "\ue7d9"
        ,
        ["pm484"] = "\ue826"
        ,
        ["shushuye"] = "\ue694"
        ,
        ["home1"] = "\ue632"
        ,
        ["jiaose"] = "\ue695"
        ,
        ["jingwuicon_svg-"] = "\ue696"
        ,
        ["role"] = "\ue61c"
        ,
        ["jiaoyiyingxiongicon"] = "\ue61d"
        ,
        ["yingxiongban"] = "\ue697"
        ,
        ["yingxiong"] = "\ue61e"
        ,
        ["baseline-home-px"] = "\ue61f"
        ,
        ["bisai"] = "\ue698"
        ,
        ["jiaose1"] = "\ue620"
        ,
        ["home2"] = "\ue622"
        ,
        ["yingxiong1"] = "\ue699"
        ,
        ["yingxiong2"] = "\ue69a"
        ,
        ["yingxiong3"] = "\ue7e8"
        ,
        ["yingxiongziliao"] = "\ue624"
        ,
        ["heroMenu"] = "\ue625"
        ,
        ["guaiwu"] = "\ue7e9"
        ,
        ["shanglu"] = "\ue627"
        ,
        ["Home"] = "\ue69e"
        ,
        ["fuzhu"] = "\ue62c"
        ,
        ["daye"] = "\ue62d"
        ,
        ["lol-sword"] = "\ue62e"
        ,
        ["zhonglu"] = "\ue62f"
        ,
        ["a-T-yingxiongzhiyehuizhang-xiaoyihuifu-05"] = "\ue69f"
        ,
        ["a-AK2yingxiongbang3"] = "\ue7ec"
        ,
        ["guaiwu2"] = "\ue6a0"
        ,
        ["guaiwu1"] = "\ue7ee"
        ,
        ["dixing"] = "\ue619"
        ,
        ["dixing1"] = "\ue683"
        ,
        ["shanchudixing"] = "\ue7cc"
        ,
        ["dixing2"] = "\ue7cd"
        ,
        ["zihuidixing"] = "\ue7ce"
        ,
        ["yidongdixing"] = "\ue7cf"
        ,
        ["yidongdixing1"] = "\ue7d0"
        ,
        ["dixing3"] = "\ue7d1"
        ,
        ["unit"] = "\ue7d2"
        ,
        ["tindixing"] = "\uec47"
        ,
        ["dixingxiugai"] = "\uec7c"
        ,
        ["TINdixingcaozuo"] = "\ueca1"
        ,
        ["tindixing1"] = "\ueca5"
        ,
        ["sifudixing"] = "\ue686"
        ,
        ["gis_dixing"] = "\ue7d3"
        ,
        ["dixing4"] = "\ue68c"
        ,
        ["dixing5"] = "\ue615"
        ,
        ["TINdixingcaozuo1"] = "\uee2a"
        ,
        ["dixing6"] = "\ue8d3"
        ,
        ["dixing7"] = "\ue68d"
        ,
        ["dixingtu"] = "\ue7f6"
        ,
        ["dixing8"] = "\ue68e"
        ,
        ["dixingxiugai1"] = "\ue7d4"
        ,
        ["dixing24_24"] = "\ue7d5"
        ,
        ["dixingwadi"] = "\uea06"
        ,
        ["sanweidixing"] = "\ue7d6"
        ,
        ["sanweidixingtu"] = "\ue690"
        ,
        ["-_dixingkaiwa"] = "\ue691"
        ,
        ["-_xiugaidixing"] = "\ue692"
        ,
        ["dixingcaijian"] = "\ue7fd"
        ,
        ["fenleidixing"] = "\ue693"
        ,
        ["dixingkaiwa"] = "\ue7d7"
        ,
        ["dixing9"] = "\ued18"
        ,
        ["yincangdixing"] = "\ue604"
        ,
        ["dixingshijing"] = "\ue991"
        ,
        ["dixingshijing1"] = "\ue999"
        ,
        ["dixingwapian"] = "\ue605"
        ,
        ["taiyang-copy"] = "\ue680"
        ,
        ["taiyang"] = "\ue681"
        ,
        ["yueliang"] = "\ue629"
        ,
        ["sun_fill"] = "\ue7ae"
        ,
        ["yueliang1"] = "\ue7af"
        ,
        ["taiyang1"] = "\ue68b"
        ,
        ["a-159_duihua-05"] = "\ue8df"
        ,
        ["a-159_wuxing"] = "\ue8e0"
        ,
        ["a-159_lianjie"] = "\ue8e1"
        ,
        ["Asclepius"] = "\ue685"
        ,
        ["Axe"] = "\ue687"
        ,
        ["SlidersHorizontal"] = "\ue7b0"
        ,
        ["GitDiff"] = "\ue7b1"
        ,
        ["GitFork"] = "\ue7b2"
        ,
        ["GitMerge"] = "\ue7b3"
        ,
        ["SunDim"] = "\ue7b4"
        ,
        ["Carrot"] = "\ue7b5"
        ,
        ["Stop"] = "\ue7b6"
        ,
        ["Sticker"] = "\ue7b7"
        ,
        ["SubsetProperOf"] = "\ue7b8"
        ,
        ["Sword"] = "\ue7b9"
        ,
        ["TelegramLogo"] = "\ue7ba"
        ,
        ["StarAndCrescent"] = "\ue7bb"
        ,
        ["DotsThree"] = "\ue7bc"
        ,
        ["DotsSix"] = "\ue7bd"
        ,
        ["DotsThreeOutline"] = "\ue7be"
        ,
        ["Checkerboard"] = "\ue7bf"
        ,
        ["Cheese"] = "\ue7c0"
        ,
        ["Sun"] = "\ue7c1"
        ,
        ["Windmill"] = "\ue7c2"
        ,
        ["LineSegments"] = "\ue7c3"
        ,
        ["EyedropperSample"] = "\ue7c4"
        ,
        ["Eyedropper"] = "\ue7c5"
        ,
        ["Farm"] = "\ue7c6"
        ,
        ["CloudArrowUp"] = "\ue7c7"
        ,
        ["CloudArrowDown"] = "\ue7c8"
        ,
        ["PaintBrush"] = "\ue7c9"
        ,
        ["Lightbulb"] = "\ue7ca"
        ,
        ["ForkKnife"] = "\ue7cb"
        ,
        ["CloudRain"] = "\ue7f8"
        ,
        ["Orange"] = "\ue819"
        ,
        ["Leaf"] = "\ue81b"
        ,
        ["Star-Fill"] = "\ue9ca"
        ,
        ["copy-02"] = "\ue606"
        ,
        ["refresh-ccw-02"] = "\ue608"
        ,
        ["end"] = "\ue609"
        ,
        ["palette"] = "\ue60a"
        ,
        ["message-chat-square"] = "\ue60b"
        ,
        ["lightbulb-02"] = "\ue60d"
        ,
        ["copy-06"] = "\ue60e"
        ,
        ["edit-03"] = "\ue611"
        ,
        ["scale-01"] = "\ue612"
        ,
        ["zhongyinhuyi"] = "\ue613"
        ,
        ["icon-test13"] = "\ue648"
        ,
        ["icon-test14"] = "\ue64a"
        ,
        ["icon-test15"] = "\ue64d"
        ,
        ["icon-test16"] = "\ue64e"
        ,
        ["icon-test17"] = "\ue651"
        ,
        ["icon-test18"] = "\ue652"
        ,
        ["icon-test19"] = "\ue653"
        ,
        ["icon-test20"] = "\ue654"
        ,
        ["icon-test21"] = "\ue655"
        ,
        ["icon-test22"] = "\ue658"
        ,
        ["icon-test23"] = "\ue659"
        ,
        ["icon-test24"] = "\ue65a"
        ,
        ["icon-test25"] = "\ue65c"
        ,
        ["icon-test26"] = "\ue65d"
        ,
        ["icon-test27"] = "\ue65e"
        ,
        ["icon-test28"] = "\ue65f"
        ,
        ["icon-test29"] = "\ue660"
        ,
        ["icon-test30"] = "\ue661"
        ,
        ["icon-test31"] = "\ue662"
        ,
        ["icon-test32"] = "\ue663"
        ,
        ["icon-test33"] = "\ue664"
        ,
        ["icon-test34"] = "\ue666"
        ,
        ["icon-test35"] = "\ue66b"
        ,
        ["icon-test36"] = "\ue66c"
        ,
        ["icon-test37"] = "\ue66d"
        ,
        ["icon-test38"] = "\ue66e"
        ,
        ["icon-test39"] = "\ue66f"
        ,
        ["icon-test40"] = "\ue670"
        ,
        ["icon-test41"] = "\ue671"
        ,
        ["icon-test42"] = "\ue673"
        ,
        ["icon-test43"] = "\ue674"
        ,
        ["icon-test44"] = "\ue676"
        ,
        ["icon-test45"] = "\ue677"
        ,
        ["icon-test46"] = "\ue679"
        ,
        ["icon-test47"] = "\ue67a"
        ,
        ["icon-test48"] = "\ue67b"
        ,
        ["safety"] = "\ue614"
        ,
        ["Security"] = "\ue798"
        ,
        ["close_circle"] = "\ue67f"
        ,
        ["collection"] = "\ue799"
        ,
        ["downland"] = "\ue79a"
        ,
        ["upload"] = "\ue79b"
        ,
        ["edit1"] = "\ue79c"
        ,
        ["eye"] = "\ue79d"
        ,
        ["no_eye"] = "\ue79e"
        ,
        ["facerecognition"] = "\ue79f"
        ,
        ["fillin"] = "\ue7a0"
        ,
        ["folder"] = "\ue7a1"
        ,
        ["more1"] = "\ue7a2"
        ,
        ["refresh"] = "\ue7a3"
        ,
        ["loop"] = "\ue7a4"
        ,
        ["send"] = "\ue7a5"
        ,
        ["fillin1"] = "\ue7a6"
        ,
        ["information_add"] = "\ue7a7"
        ,
        ["target"] = "\ue7a8"
        ,
        ["textdeletion"] = "\ue7a9"
        ,
        ["text"] = "\ue7aa"
        ,
        ["trash"] = "\ue7ab"
        ,
        ["type"] = "\ue7ac"
        ,
        ["1086585"] = "\ue7ad"
        ,
        ["duizhan"] = "\ue61b"
        ,
        ["scala-icon"] = "\ue60c"
        ,
        ["check"] = "\ue646"
        ,
        ["close"] = "\ue647"
        ,
        ["edit"] = "\ue649"
        ,
        ["favorfill"] = "\ue64b"
        ,
        ["favor"] = "\ue64c"
        ,
        ["loading"] = "\ue64f"
        ,
        ["locationfill"] = "\ue650"
        ,
        ["roundcheckfill"] = "\ue657"
        ,
        ["comment"] = "\ue667"
        ,
        ["likefill"] = "\ue668"
        ,
        ["like"] = "\ue669"
        ,
        ["evaluate"] = "\ue672"
        ,
        ["wang"] = "\ue678"
        ,
        ["cascades"] = "\ue67d"
        ,
        ["discover"] = "\ue67e"
        ,
        ["list"] = "\ue682"
        ,
        ["more"] = "\ue684"
        ,
        ["scan"] = "\ue689"
        ,
        ["settings"] = "\ue68a"
        ,
        ["pic"] = "\ue69c"
        ,
        ["footprint"] = "\ue69d"
        ,
        ["moreandroid"] = "\ue76f"
        ,
        ["deletefill"] = "\ue770"
        ,
        ["qrcode"] = "\ue771"
        ,
        ["delete"] = "\ue772"
        ,
        ["homefill"] = "\ue773"
        ,
        ["message"] = "\ue774"
        ,
        ["addressbook"] = "\ue775"
        ,
        ["activity"] = "\ue776"
        ,
        ["friendaddfill"] = "\ue777"
        ,
        ["friendadd"] = "\ue778"
        ,
        ["friendfamous"] = "\ue779"
        ,
        ["friend"] = "\ue77a"
        ,
        ["selection"] = "\ue77b"
        ,
        ["tmall"] = "\ue77c"
        ,
        ["lightauto"] = "\ue77d"
        ,
        ["lightforbid"] = "\ue77e"
        ,
        ["lightfill"] = "\ue77f"
        ,
        ["camerarotate"] = "\ue780"
        ,
        ["light"] = "\ue781"
        ,
        ["barcode"] = "\ue782"
        ,
        ["flashlightclose"] = "\ue783"
        ,
        ["flashlightopen"] = "\ue784"
        ,
        ["clothes"] = "\ue785"
        ,
        ["creative"] = "\ue786"
        ,
        ["female"] = "\ue787"
        ,
        ["rank"] = "\ue788"
        ,
        ["icon_safe"] = "\ue789"
        ,
        ["bad"] = "\ue78a"
        ,
        ["focus"] = "\ue78b"
        ,
        ["peoplefill"] = "\ue78c"
        ,
        ["people"] = "\ue78d"
        ,
        ["cut"] = "\ue78e"
        ,
        ["magic"] = "\ue78f"
        ,
        ["group"] = "\ue790"
        ,
        ["hotfill"] = "\ue791"
        ,
        ["hot"] = "\ue792"
        ,
        ["post"] = "\ue793"
        ,
        ["radiobox"] = "\ue794"
        ,
        ["writefill"] = "\ue795"
        ,
        ["write"] = "\ue796"
        ,
        ["safe"] = "\ue797"
        ,
        ["skin_light"] = "\ue7da"
        ,
        ["search_light"] = "\ue7db"
        ,
        ["scan_light"] = "\ue7dc"
        ,
        ["people_list_light"] = "\ue7dd"
        ,
        ["message_light"] = "\ue7de"
        ,
        ["close_light"] = "\ue7df"
        ,
        ["add_light"] = "\ue7e0"
        ,
        ["profile_light"] = "\ue7e1"
        ,
        ["friend_add_light"] = "\ue7e2"
        ,
        ["hot_light"] = "\ue7e3"
        ,
        ["share_light"] = "\ue7e4"
        ,
        ["comment_light"] = "\ue7e5"
        ,
        ["favor_light"] = "\ue7e6"
        ,
        ["friend_light"] = "\ue7e7"
        ,
        ["global_light"] = "\ue7ea"
        ,
        ["global"] = "\ue7eb"
        ,
        ["delete_light"] = "\ue7ed"
        ,
        ["coffee"] = "\ue7f1"
        ,
        ["sports"] = "\ue7f2"
        ,
        ["dunpai"] = "\ue631"
        ,
        ["quanxianguanli"] = "\ue675"
        ,
        ["icon-test1"] = "\ue634"
        ,
        ["icon-test2"] = "\ue635"
        ,
        ["icon-test3"] = "\ue636"
        ,
        ["icon-test4"] = "\ue637"
        ,
        ["icon-test5"] = "\ue63b"
        ,
        ["icon-test6"] = "\ue63c"
        ,
        ["icon-test7"] = "\ue63d"
        ,
        ["icon-test8"] = "\ue640"
        ,
        ["icon-test9"] = "\ue641"
        ,
        ["icon-test10"] = "\ue642"
        ,
        ["icon-test11"] = "\ue643"
        ,
        ["icon-test12"] = "\ue644"
        ,
        ["baocun"] = "\ue6f7"
        ,
        ["gengduo"] = "\ue6f9"
        ,
        ["gongxiang"] = "\ue6fa"
        ,
        ["lingcunwei"] = "\ue6fb"
        ,
        ["caidan"] = "\ue6fc"
        ,
        ["saoma"] = "\ue6fd"
        ,
        ["wenjiancuowu"] = "\ue6fe"
        ,
        ["riqi"] = "\ue6ff"
        ,
        ["wenjiandaochu"] = "\ue700"
        ,
        ["shijian"] = "\ue701"
        ,
        ["yingyong"] = "\ue702"
        ,
        ["wenjianjia"] = "\ue703"
        ,
        ["wenjianqueren"] = "\ue704"
        ,
        ["wenjiandaoru"] = "\ue705"
        ,
        ["a-yinzhangshenhe"] = "\ue706"
        ,
        ["yuyin"] = "\ue707"
        ,
        ["wenjianjiaxinzeng"] = "\ue709"
        ,
        ["zhongmingming"] = "\ue70b"
        ,
        ["wenjianjiashanchu"] = "\ue70c"
        ,
        ["biaoqian"] = "\ue70d"
        ,
        ["a-biaoqingweixiao"] = "\ue70e"
        ,
        ["biaodan"] = "\ue70f"
        ,
        ["diannao"] = "\ue710"
        ,
        ["linggan"] = "\ue711"
        ,
        ["shouji"] = "\ue712"
        ,
        ["bingtu"] = "\ue713"
        ,
        ["bangong"] = "\ue714"
        ,
        ["dianhua"] = "\ue715"
        ,
        ["shipin"] = "\ue716"
        ,
        ["xiangmu"] = "\ue717"
        ,
        ["yincang"] = "\ue718"
        ,
        ["xianshi"] = "\ue719"
        ,
        ["zhuzhuangtu"] = "\ue71a"
        ,
        ["yunshangchuan"] = "\ue71b"
        ,
        ["zhexiantu"] = "\ue71c"
        ,
        ["yunxiazai"] = "\ue71d"
        ,
        ["zhinan"] = "\ue71e"
        ,
        ["yibiaopan"] = "\ue71f"
        ,
        ["zhuti"] = "\ue720"
        ,
        ["anquangaojing"] = "\ue721"
        ,
        ["daiban"] = "\ue722"
        ,
        ["dian"] = "\ue723"
        ,
        ["gouwuche"] = "\ue724"
        ,
        ["dianlianquan"] = "\ue725"
        ,
        ["gouwulan"] = "\ue726"
        ,
        ["anquan"] = "\ue727"
        ,
        ["qingchu"] = "\ue728"
        ,
        ["youzhankai"] = "\ue729"
        ,
        ["hetong"] = "\ue72a"
        ,
        ["shui"] = "\ue72b"
        ,
        ["huo"] = "\ue72c"
        ,
        ["shoukuan2_1"] = "\ue72d"
        ,
        ["zijin2"] = "\ue72e"
        ,
        ["shoukuan2"] = "\ue72f"
        ,
        ["zijin3"] = "\ue730"
        ,
        ["zijin4"] = "\ue731"
        ,
        ["zijin1"] = "\ue732"
        ,
        ["zijinanquan"] = "\ue733"
        ,
        ["zuosuoxiao"] = "\ue734"
        ,
        ["biji"] = "\ue735"
        ,
        ["chuangzuo"] = "\ue736"
        ,
        ["feiji"] = "\ue738"
        ,
        ["daima"] = "\ue739"
        ,
        ["huoche"] = "\ue73a"
        ,
        ["gouwu"] = "\ue73b"
        ,
        ["huojian"] = "\ue73c"
        ,
        ["jiagou"] = "\ue73d"
        ,
        ["a-querenqueding"] = "\ue73f"
        ,
        ["qi"] = "\ue740"
        ,
        ["lichengpai"] = "\ue741"
        ,
        ["paizhao"] = "\ue742"
        ,
        ["qiche"] = "\ue743"
        ,
        ["shexiangtou"] = "\ue744"
        ,
        ["wanchenggongdan"] = "\ue745"
        ,
        ["jiangbei"] = "\ue746"
        ,
        ["tongxunlu"] = "\ue747"
        ,
        ["zuzhi"] = "\ue748"
        ,
        ["shipintonghua"] = "\ue749"
        ,
        ["shoukuan"] = "\ue74a"
        ,
        ["dayinji"] = "\ue74d"
        ,
        ["dayu"] = "\ue74e"
        ,
        ["a-peizhicanshu"] = "\ue74f"
        ,
        ["daxue"] = "\ue750"
        ,
        ["duoyun"] = "\ue751"
        ,
        ["quxiaogongxiang"] = "\ue752"
        ,
        ["leizhenyu"] = "\ue753"
        ,
        ["xiaoyu"] = "\ue754"
        ,
        ["a-qingtaiyang"] = "\ue755"
        ,
        ["yejianduoyun"] = "\ue756"
        ,
        ["wangluo"] = "\ue757"
        ,
        ["xiaoxue"] = "\ue758"
        ,
        ["yin"] = "\ue759"
        ,
        ["xiangmufuhao"] = "\ue75a"
        ,
        ["zhongyu"] = "\ue75b"
        ,
        ["a-yejianyueliang"] = "\ue75c"
        ,
        ["yujiaxue"] = "\ue75d"
        ,
        ["zhongxue"] = "\ue75e"
        ,
        ["gongdan"] = "\ue75f"
        ,
        ["shu"] = "\ue760"
        ,
        ["kaiguan"] = "\ue761"
        ,
        ["fuwuqi"] = "\ue762"
        ,
        ["xuexi"] = "\ue763"
        ,
        ["moxing"] = "\ue764"
        ,
        ["fenxiang"] = "\ue765"
        ,
        ["zhineng"] = "\ue766"
        ,
        ["shujuku"] = "\ue767"
        ,
        ["a-zifuwenben"] = "\ue768"
        ,
        ["xinduihua"] = "\ue769"
        ,
        ["a-shaloudengdai"] = "\ue76a"
        ,
        ["wuxinhao"] = "\ue76b"
        ,
        ["xinhao"] = "\ue76c"
        ,
        ["niantie_1"] = "\ue76d"
        ,
        ["a-zujianchajian"] = "\ue76e"
        ,
        ["icon13"] = "\ue60f"
        ,
        ["texiao"] = "\ue73e"
        ,
        ["zhuangbeibao01"] = "\ue63e"
        ,
        ["jian"] = "\ue66a"
        ,
        ["wodezhuangbeiku"] = "\ue645"
        ,
        ["texiao1"] = "\ue74b"
        ,
        ["zhuangbeiqianghua"] = "\ue973"
        ,
        ["texiao2"] = "\ue638"
        ,
        ["texiao3"] = "\ue65b"
        ,
        ["icon-test"] = "\ue656"
        ,
        ["daohang_zhuangbeiguanli"] = "\ue688"
        ,
        ["chuiziicon"] = "\ue62a"
        ,
        ["shuxing-shouqi"] = "\ue639"
        ,
        ["texiao4"] = "\ue621"
        ,
        ["texiao5"] = "\ue630"
        ,
        ["zhuangbei"] = "\ue626"
        ,
        ["zhuangbei1"] = "\ue600"
        ,
        ["texiao6"] = "\ue610"
        ,
        ["zhuangbei2"] = "\ue6d4"
        ,
        ["zhuangbei_huaban"] = "\ue6f8"
        ,
        ["wodezhuangbeiku1"] = "\ue601"
        ,
        ["gong"] = "\ue62b"
        ,
        ["texiao7"] = "\ue6b7"
        ,
        ["zhuangbei3"] = "\ue6e5"
        ,
        ["mofabang"] = "\ue633"
        ,
        ["wodezhuangbeiku2"] = "\ue602"
        ,
        ["zhuangbeiqianghua1"] = "\ue628"
        ,
        ["bangqiu"] = "\ue618"
        ,
        ["zhuangbei4"] = "\ue623"
        ,
        ["-axe-"] = "\ue74c"
        ,
        ["duixiangshuxingObjectAttributes1"] = "\ue6cb"
        ,
        ["texiao8"] = "\ue6af"
        ,
        ["jian1"] = "\ue737"
        ,
        ["gongnu"] = "\ue617"
        ,
        ["a-69"] = "\ue920"
        ,
        ["gongjian"] = "\ue67c"
        ,
        ["wuqizhuangbei"] = "\ue7d8"
        ,
        ["a-shenpanchuizi"] = "\ue63f"
        ,
        ["zhuangbei5"] = "\ue708"
        ,
        ["texiao9"] = "\ue665"
        ,
        ["fuzi"] = "\ue63a"
        ,
        ["zhuangbeixiangqing"] = "\ue603"
        ,
        ["zhuangbeiliebiao"] = "\ue607"
        ,
        ["a-44tubiao-119"] = "\ue69b"
        ,
        ["a-44tubiao-230"] = "\ue6a5"
        ,
        ["jixiezhuangbei"] = "\ue70a"
        ,
        ["zhuangbei6"] = "\ue68f"
        ,
        ["texiao10"] = "\ue902"
        ,
        ["bianji"] = "\ue6a1"
        ,
        ["fangda"] = "\ue6a2"
        ,
        ["guanbi"] = "\ue6a3"
        ,
        ["jian2"] = "\ue6a4"
        ,
        ["jiesuo"] = "\ue6a6"
        ,
        ["a-jiaxinzeng"] = "\ue6a7"
        ,
        ["a-quanxianyuechi"] = "\ue6a8"
        ,
        ["daoru"] = "\ue6a9"
        ,
        ["a-shanchuhuishou"] = "\ue6aa"
        ,
        ["xiala"] = "\ue6ab"
        ,
        ["suoxiao"] = "\ue6ac"
        ,
        ["tuichu"] = "\ue6ad"
        ,
        ["shezhi"] = "\ue6ae"
        ,
        ["fuzhi"] = "\ue6b0"
        ,
        ["xiazai"] = "\ue6b1"
        ,
        ["shangchuan"] = "\ue6b2"
        ,
        ["youjiantou"] = "\ue6b3"
        ,
        ["suoding"] = "\ue6b4"
        ,
        ["daochu"] = "\ue6b5"
        ,
        ["chehui"] = "\ue6b6"
        ,
        ["fasong"] = "\ue6b8"
        ,
        ["gaojing"] = "\ue6b9"
        ,
        ["huifu"] = "\ue6ba"
        ,
        ["jinyong"] = "\ue6bb"
        ,
        ["kaishi"] = "\ue6bc"
        ,
        ["bangzhu"] = "\ue6bd"
        ,
        ["a-dingweiweizhi"] = "\ue6be"
        ,
        ["a-gaojingxiaoxi"] = "\ue6bf"
        ,
        ["qiehuan"] = "\ue6c0"
        ,
        ["shuaxin"] = "\ue6c1"
        ,
        ["paixu"] = "\ue6c2"
        ,
        ["shaixuan"] = "\ue6c3"
        ,
        ["gonggao"] = "\ue6c4"
        ,
        ["tingzhi"] = "\ue6c5"
        ,
        ["xiaoxi"] = "\ue6c6"
        ,
        ["zanting"] = "\ue6c7"
        ,
        ["sousuo"] = "\ue6c8"
        ,
        ["xinxi"] = "\ue6c9"
        ,
        ["youjian"] = "\ue6ca"
        ,
        ["chuizhifenbu"] = "\ue6cc"
        ,
        ["cai"] = "\ue6cd"
        ,
        ["chuizhijuzhongduiqi"] = "\ue6ce"
        ,
        ["diduiqi"] = "\ue6cf"
        ,
        ["dianzan"] = "\ue6d0"
        ,
        ["dingduiqi"] = "\ue6d1"
        ,
        ["quanping"] = "\ue6d2"
        ,
        ["quxiaolianjie"] = "\ue6d3"
        ,
        ["a-huanyuanquxiaoquanping"] = "\ue6d5"
        ,
        ["xihuan"] = "\ue6d6"
        ,
        ["shoucang"] = "\ue6d7"
        ,
        ["zuoduiqi"] = "\ue6d8"
        ,
        ["wenziyouduiqi"] = "\ue6d9"
        ,
        ["youduiqi"] = "\ue6da"
        ,
        ["wenzizuoduiqi"] = "\ue6db"
        ,
        ["shuipingfenbu"] = "\ue6dc"
        ,
        ["shuipingjuzhongduiqi"] = "\ue6dd"
        ,
        ["lianjie"] = "\ue6de"
        ,
        ["guanji"] = "\ue6df"
        ,
        ["a-zhuyeshouye"] = "\ue6e0"
        ,
        ["kehu"] = "\ue6e1"
        ,
        ["tupianshangchuan"] = "\ue6e2"
        ,
        ["wenjian"] = "\ue6e3"
        ,
        ["wenjianbanben"] = "\ue6e4"
        ,
        ["wenjiangaojing"] = "\ue6e6"
        ,
        ["wenjianbianji"] = "\ue6e7"
        ,
        ["tupian"] = "\ue6e8"
        ,
        ["xinzengyonghu"] = "\ue6e9"
        ,
        ["shanchuyonghu"] = "\ue6ea"
        ,
        ["wenjianshangchuan"] = "\ue6eb"
        ,
        ["wenjiansousuo"] = "\ue6ec"
        ,
        ["wenjianxiazai"] = "\ue6ed"
        ,
        ["wenjianxinzeng"] = "\ue6ee"
        ,
        ["wenjianlingcun"] = "\ue6ef"
        ,
        ["tupianxiazai"] = "\ue6f0"
        ,
        ["wenziliangduanduiqi"] = "\ue6f1"
        ,
        ["yonghu"] = "\ue6f2"
        ,
        ["wenjianshanchu"] = "\ue6f3"
        ,
        ["yonghuqunzu"] = "\ue6f4"
        ,
        ["wenzijuzhongduiqi"] = "\ue6f5"
        ,
        ["biaoge"] = "\ue6f6"
    };

    /// <summary>根据图标键名获取对应的图标字符</summary>
    public static string? Get(string key)
        => _map.TryGetValue(key, out var v) ? v : null;

    /// <summary>获取所有图标键名列表</summary>
    public static IReadOnlyList<string> AllKeys { get; } = _map.Keys.ToList();
}
