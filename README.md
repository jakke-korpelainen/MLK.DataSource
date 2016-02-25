# MLK.DataSource
Parser for open times at Meilahden Liikuntakeskus for badminton and tennis

Basically reads in and parses https://www.slsystems.fi/meilahti/ftpages/ft-varaus-index-01.php with query parameters for date (?date=yyyy-mm-dd) and the sports type (?laji=1/2 1: tennis 2:badminton).
Returns list of objects containing the data for the field, time and sports type.
