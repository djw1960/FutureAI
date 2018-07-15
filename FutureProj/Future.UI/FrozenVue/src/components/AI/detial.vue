<template>
    <div>
        <form action="#">
                    <div class="ui-form-item ui-border-b">
                        <label>
                            列表标题
                        </label>
                        <input type="text" placeholder="18位身份证号码">
                        <a href="#" class="ui-icon-close">
                        </a>
                    </div>
                    <div class="ui-form-item ui-form-item-link ui-border-b">
                        <label>
                            列表标题
                        </label>
                    </div>
                    <div class="ui-form-item ui-form-item-link ui-border-b">
                        <label>
                            标题
                        </label>
                    </div>
                    <div class="ui-btn-wrap">
                        <button class="ui-btn-lg ui-btn-primary">
                            确定
                        </button>
                    </div>
                    <div class="ui-form-item ui-form-item-switch ui-border-b">
                        <p>
                            对附近的人可见
                        </p>
                        <label class="ui-switch">
                            <input type="checkbox">
                        </label>
                    </div>
                    <div class="ui-form-item ui-border-b">
                        <label>日期</label>
                        <div class="ui-select-group">
                            <div class="ui-select">
                                <select>
                                    <option>2014</option>
                                    <option selected="">2015</option>
                                    <option>2016</option>
                                </select>
                            </div>
                            <div class="ui-select">
                                <select>
                                    <option>03</option>
                                    <option selected="">04</option>
                                    <option>05</option>
                                </select>
                            </div>
                            <div class="ui-select">
                                <select>
                                    <option>21</option>
                                    <option selected="">22</option>
                                    <option>23</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="ui-form-item ui-form-item-radio ui-border-b">

                        <label class="ui-radio" for="radio">
                            <input type="radio" name="radio">
                        </label>
                        <p>表单中用于单选操作</p>
                    </div>
                </form>
    </div>
</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    import {formatDate} from '@/Common/data.js'
    export default {
        data(){
            return{
                userEntity:{},
                menus:[],
                id:this.$route.params.id
            }
        },
        filters:{
            formatDate(time){
                let dt=new Date(time);
                return formatDate(dt,'MM.dd hh');
            }
        },
        methods:{
            loadAIInfo(){
                var self=this;
                var params = {
                    no: 2061,
                    token:self.userEntity.token,
                    id:self.id,
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        console.log(resp.data.data);
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }
        },
        created(){
            // 取值时：把获取到的Json字符串转换回对象
            var userJsonStr = sessionStorage.getItem('user');
            this.userEntity = JSON.parse(userJsonStr);
            var mlist= JSON.parse(sessionStorage.getItem('menulist'));
            for (let index = 0; index < mlist.length; index++) {
                const element = mlist[index];
                if (element.Mode=="ai") {
                    this.menus.push(element);
                }
            }
            this.loadAIInfo();
        }
    }
</script>

<style scoped>
.mgreen{
    color: green;
}
.mlose{
    background-color: darkgray;
    color: white;
}
.mwin{
    background-color: darkorange;
    color: white;
}
</style>