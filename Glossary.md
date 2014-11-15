##Web services

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

Teljið upp nokkur HTTP verbs og hvaða tilgangi þau þjóna
* GET (sækir gögn), POST (býr til nýja gögn), PATCH (uppfærir gögn, t.d. eitt svæði í töflu), PUT (uppfærir gögn í heild sinni), DELETE (eyðir eða setur delete flagg á true)
* HTTP verb tilgreina hvaða aðgerð á að framkvæma

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

Hvernig er best að útgáfustýra API-um
* Setja útgáfuna í urlið /api/v1/...  mikið notað
* Clienter bæti vðið custom http header við request sem segir að þeir vilja fá version x, erfitt að prófa með curl eða vafra
* Content type, bæta þessu við í Accept http headerinn 

##ASP.NET Web API
Hvað heitir nýja web apið sem Microsoft er að vinna að núna 
* ASP.NET vNext
* Open source og er á GtiHub
* Sameinar Web Forms, MVC, Web API í eina sæng, eða réttara sagt verið er að endurskrifa þetta
* Getur keyrt á öllum stýrikerfum

Hvernig er arkitektúr Web API 
* Allir controller klasar verða að erfa frá ApiController 
* ValuController sér um að mappa Urlin í kóðanum, ef föll í controllert byjra á Get, Put, Post er gengið út frá því að þau höndli þessi atburði, það er líka hægt að skilgreina með attribute fyrir ofan fallið hvaða verb það höndlar [HttpGet] etc

Hvernig er það stillt hvort controller skili JSON eða XML t.d. 
* Accept Http Hedar inniheldur Mime type sem segir til um þetta, application/json, applicatoin/xml

Hvað er CORS (Cross-Origin Resource Sharing)?
* Web apar setja Access-Control-Allow-Origin" HTTP header í response til að tilgreina hvað þeir styðja, þ.e. þa er hægt að tilgreina hvaða domain mega kalla í apan, hvaða aðgerðir eru í boði etc. By default þá gengur ekki að kalla í Apa sem er á öðru domain en maður sjálfur. 
* Vafrar (Chorme, Firefox etc.) senda fyrst HTTP Options request til að vita hvað þeir mega gera, fá til baka "Access-Control-Allow-Origin" HTTP header sem tilgreinir hvort þeir mega kalla í Apan og hvaða aðgerðir má nota.   
* var cors = new EnableCorsAttribute("*", "*", "*");  --origin, header and methods 

##Routing

Lýsið default routing í ASP.NET Web API
* URL eiga að stemma við ControllerName/ActionName 

Lýsið attribute routing
* Route prefix er skilgreint fyrir ofan controller klasan [RoutePrefix("api/v1/my")], síðan er hver aðgerð í klasanum tengd við ákveðna http verb aðgerð, [Route("grades")] etc.  Það er líka mögulegt að láta aðgerðirnar heita Get.., Post.. osfrv. 

Hvernig má senda paramert í api call 
* Parametra má skilgreina í routing fyrir fall, [Route("grades/{id:int}")], síðan verður fallið sem hefur þessa routing skilgreiningu að taka inn parameter sem heitir nákvæmlega það sama

Hvernig má bæta við query parameter í api call
* það er gert með spurningamerki /api/v1/12345/grades?semester=20133, þá verður fallið að taka inn parameter sem hetiri semester og hann getur verið optional eða ekki



