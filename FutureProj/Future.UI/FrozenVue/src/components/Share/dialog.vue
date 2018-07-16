<template>
<div class="ui-dialog ui-dialog-operate show">
    <div class="ui-dialog-cnt">
        <div class="ui-dialog-bd">
            <h3>{{modal.title}}</h3>
            <p v-html="modal.text"></p>
        </div>
        <div class="ui-dialog-ft">
            <button type="button" data-role="button" class="ui-btn" @click="cancel">{{modal.cancelButtonText}}</button>
            <button type="button" data-role="button" class="ui-btn" @click="submit">{{modal.confirmButtonText}}</button>
        </div>
        <i class="ui-dialog-close" data-role="button" @click="cancel"></i>
    </div>
</div>
</template>

<script>
    export default {
    name: 'mdialog',
    props: {
        dialogOption: Object
    },
    data() {
        return {
            resolve: '',
            reject: '',
            promise: '', // 保存promise对象
        }
    },
    computed: {
        modal: function() {
            let options = this.dialogOption;
            return {
                title: options.title || '提示',
                text: options.text,
                cancelButtonText: options.cancelButtonText ? options.cancelButtonText : '取消',
                confirmButtonText: options.confirmButtonText ? options.confirmButtonText : '确定',
            }
        }
    },
    methods: {
        //确定,将promise断定为完成态
        submit() {
            this.resolve('submit');
        },
        // 取消,将promise断定为reject状态
        cancel() {
            this.reject('cancel');
        },
        //显示confirm弹出,并创建promise对象，给父组件调用
        confirm() {
            this.promise = new Promise((resolve, reject) => {
                this.resolve = resolve;
                this.reject = reject;
            });
            return this.promise; //返回promise对象,给父级组件调用
        }
    }
    }
</script>

<style>
.mfield{
    background-color: darkgray;
    color: white;
    font-size: small;
    padding: 0 5px 0 10px;
}
.mfieldkey{
    font-size: 0.1rem;
    color:yellow;
}
.mfieldvalue{
    font-size: 0.1rem;
    color:darkmagenta;
}
</style>