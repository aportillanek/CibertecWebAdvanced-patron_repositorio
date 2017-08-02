
var self = this;

self.onmessage = function (message)
{
    
    var splittextLine = message.data.split(",");

    var member = {
        member_no: splittextLine[0],
        lastname: splittextLine[1],
        firstname: splittextLine[2],
        middleinitial: splittextLine[3],
        street: splittextLine[4],
        city: splittextLine[5],
        state_prov: splittextLine[6],
        country: splittextLine[7],
        mail_code: splittextLine[8],
        phone_no: splittextLine[9],
        issue_dt: splittextLine[10],
        expr_dt: splittextLine[11],
        corp_no: splittextLine[12],
        prev_balance: splittextLine[13],
        curr_balance: splittextLine[14],
        member_code: splittextLine[15]

    }
    self.postMessage(member);

}


