<template>
<div>
    <div class="header">
        
    </div>
<section id="tasklist">
    <table class="ui-table ui-border">
            <tr class="">
                <td class="" data-href="component/button/btn.html">
                    <div>
                        <div class="icon-button icon"></div>
                        <div class="tit">按钮</div>
                        <div class="sub-tit">Button</div>
                    </div>
                </td>
                <td class="" data-href="component/list/list.html">
                    <div>
                        <div class="icon-cell icon"></div>
                        <div class="tit">列表</div>
                        <div class="sub-tit">List</div>
                    </div>
                </td>
                <td data-href="component/tips/tips.html">
                    <div>
                        <div class="icon-toast icon"></div>
                        <div class="tit">提示</div>
                        <div class="sub-tit">Tips</div>
                    </div>
                </td>
            </tr>
    </table>
</section>
</div>

</template>

<script>
    export default {
        data(){
            return {

            }
        },
        methods:{

        }
    }
</script>

<style scoped>
#tasklist>table td{
     width: 33.33%;
    position: relative;
}

</style>