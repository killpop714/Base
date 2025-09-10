

let num = 0;
const $btn = document.querySelector('#Checkbtn');
const $input = document.querySelector('#i1');
const $order = document.querySelector("#order");
const $word = document.querySelector("#word");
const $time = document.querySelector("#time");
const $output =document.querySelector('#output');


let data;//현재 단어
let word;//단어(제시어)
 //API에 들어갈 단어 변수



//시간 설정
let timesec=0;
$time.textContent = timesec;
let flag = false;
let Be =true;
TimeOut();

//순서 문한 루프
while(isNaN(num) || num<1 || !num)
{
    num = Number(prompt("참가자는 몇명인가요?"));
    if(num<=0)
        alert("다시 입력해주세요.");
}

//순서
$order.textContent = 1;
$word.textContent = "1번째 순서입니다.";

const inputdata = (e) =>
{
    data= e.target.value
};



const Output =() =>
 {
    // Data.then(d => console.log(d.channel.item[0].word.replace("-","")));
    if( !word && data.length === 3 || word[word.length -1]== data[0])
    {
        const apiKey = 	'48E081530EA6C2E51EBD31A0C5AD12A9';
        fetch(`http://opendict.korean.go.kr/api/search?certkey_no=4989&key=${apiKey}&target_type=search&req_type=json&part=word&q=${data}&sort=dict&start=1&num=10`)
        .then(j=>j.json())
        .then(d=>{
            console.log(d.channel.item[0].word.replace("-",""))
                if(data==d.channel.item[0].word.replace("-",""))
                {
                    
                    timesec =-1;
                     //데이터 대입
                    flag =true;
                    word = data;3
                    $word.textContent = data;

                    //순서
                    const order = Number($order.textContent);
                    if(order + 1>num){
                    $order.textContent = 1;
                    }
                    else
                    {
                        $order.textContent = order + 1;
                    }
                    $input.value ="";
                    $input.focus();  
        }})
        .catch(err =>alert("데이터가 비어있습니다."+" 오류:"+err));
        
    }
    else
    {
        alert("중복 또는 잘못된 값이니다.");     
        $input.value ="";
        $input.focus();
    }


    

}


function TimeOut(){
    setInterval(()=>{

        ++timesec;
        if(flag === false)
            timesec = 0;

        if(timesec >5.)
        {         
            alert("시간 아웃")
            $input.value ="";
            $input.focus();
            timesec =-1;            
        }   
         $time.textContent = timesec;
    },1000) 
}


$input.addEventListener('input', inputdata);
$btn.addEventListener('click',Output);


