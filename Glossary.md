#Web services

Hverjir eru kostirnir við vefþjónustur?
* Þær eru cross platform, þ.e. það geta margar tengudir af clientum tengst þeim
* Sá sem á gögnin getur exposað þeim og aðrir sem kunna eða finna sniðuga lausn til að nýta gögnin geta gert það.
* Mass up consept, taka tvö eða fleiri API og sameina þau, búa til eitthvað sniðugt
* Allri buisness lógík þarf bara að viðhalda á einum stað
* Clientin getur verið mjög léttur og þarf bara að rendera eitt HTML
* API eða vefþjónustur get svo exposað gögnum og allir (margir) get nýtt sér það

Hverjir geta verið ókostirnir við vefþjónustur?
* Getur valdið því að þegar smá breytingar verða á html síðu að þá verði client að reloda henni að fullu frá þjónustunni
* Ef clientin er ekki vefur þá getur hann þurft að parsa HTML-ið til að ná í gögnin, það býður villum heim þar sem HTML getur breyst og þá virkar kannski ekki parsing lengur. 

Nefnið 3 algengar gerðir af vefþjónustum og lýsið arkitektúr þeirra stuttlega
* REST (REpresentational State Transfer, Roy Fielding in 2000) þjónustur eru client/server, þær eru stateless, þær eru cachable, og hypertext driven.   
* SOAP 
* WCF

Teljið upp nokkur HTTP verbs
* GET (sækir gögn), POST (býr til nýja gögn), PATCH (uppfærir gögn, t.d. eitt svæði í töflu), PUT (uppfærir gögn í heild sinni), DELETE (eyðir eða setur delete flagg á true)

Lýsið hefbundinni REST þjónustu í dag
* REST er HTTP þjónusta, sækir gögn og skilar JSON, það er lang algengasta útfærsluaðferð á REST í dag.  En REST þjónusta skv. skilgreiningu þarf ekki að nota HTTP.  Með REST er fólk að uppgvöta hvernig HTTP á að virka, þar sem verbin eru notuð mikið í REST þjónustum. 

Nefnið nokkra HTTP villukóða
* 200 aðgerð heppnaðist
* 301 redirect, urlið var fært, líklega ekki mikið notað
* 400 bad request, client gerði eitthvað vitlaust 
* 401 auðkennin ekki í lagi, senda HTTP authrozation header með
* 403 notandi má ekki framkvæma aðgerðina
* 404 gögn fundust ekki 
* 500 server villa, client getur ekkert gert í þessu

Hvernig á að byggja upp URL fyrir REST þjónustu ?
* /api/courses, sækir alla áfanga
* /api/courses/26892, sækir einn tiltekin áfanga
* /api/courses/26892/students, sækir alla nemendur í tilteknum áfanga

Hvernig er hægt að gera URL óhakkanleg (HATEOAS)
* Mögulega hægt að gera með ólæsanlegum URL-um
* Eða apin muni bara exposa rótar-urlinu og síðan þyrfti að kalla í Apan til að komast að því hvað er hægt að gera, rótarkallið skilar því

Hvernig er hægt að skilgreina hversu mikið þjónusta er REST þjónusta
* Level 0, SOAP, eitt url, allataf gert POST fyrirspurn á SOAP, uppl sendar í requesti um hvaða aðgerð er verið að kalla á
* Level 1, mismuanndi URL búið að bæta við resource með því
* Level 2, búið að bæta við HTTP verb, Post, Get etc. 
* Level 3, eitt rótar-url, notendur verða að kalla á þjónustuna til að komast að því hvað er hægt að gera, hér er möguleiki að hafa url sem er óhakkanleg (HATEOAS)
* Flestar vefþjónustur eru á level 2 í dag




