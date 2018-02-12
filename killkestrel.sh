sudo lsof -iTCP -sTCP:LISTEN -P | grep :44304
// take port value from previous call and call sudo kill -i <port #>
sudo lsof -iTCP -sTCP:LISTEN -P | grep :44399
// do prior kill for this app too
